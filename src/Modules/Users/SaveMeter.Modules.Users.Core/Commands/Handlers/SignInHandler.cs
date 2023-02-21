using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using SaveMeter.Modules.Users.Core.Entities;
using SaveMeter.Modules.Users.Core.Exceptions;
using SaveMeter.Modules.Users.Core.Repositories;
using SaveMeter.Shared.Abstractions;
using SaveMeter.Shared.Abstractions.Auth;
using SaveMeter.Shared.Abstractions.Commands;
using SaveMeter.Shared.Abstractions.Messaging;

namespace SaveMeter.Modules.Users.Core.Commands.Handlers;
internal class SignInHandler : ICommandHandler<SignIn, JsonWebToken>
{
    private readonly IUserRepository _userRepository;
    private readonly IAuthManager _authManager;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly IMessageBroker _messageBroker;
    private readonly ILogger<SignInHandler> _logger;

    public SignInHandler(IUserRepository userRepository, IAuthManager authManager,
        IPasswordHasher<User> passwordHasher, IMessageBroker messageBroker,
        ILogger<SignInHandler> logger)
    {
        _userRepository = userRepository;
        _authManager = authManager;
        _passwordHasher = passwordHasher;
        _messageBroker = messageBroker;
        _logger = logger;
    }

    public async Task<JsonWebToken> HandleAsync(SignIn command, CancellationToken cancellationToken = default)
    {
        var user = await _userRepository.GetAsync(command.Email.ToLowerInvariant())
            .NotNull(() => new InvalidCredentialsException());

        if (user.State != UserState.Active)
        {
            throw new UserNotActiveException(user.Id);
        }

        if (_passwordHasher.VerifyHashedPassword(default, user.Password, command.Password) ==
            PasswordVerificationResult.Failed)
        {
            throw new InvalidCredentialsException();
        }

        var claims = new Dictionary<string, IEnumerable<string>>
        {
            ["permissions"] = user.Roles.SelectMany(x => x.Permissions)
        };

        var jwt = _authManager.CreateToken(user.Id, string.Join(' ', user.Roles.Select(x => x.Name)), claims: claims);
        jwt.Email = user.Email;
        _logger.LogInformation($"User with ID: '{user.Id}' has signed in.");
        return jwt;
    }
}
