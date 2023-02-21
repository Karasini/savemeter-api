using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;
using SaveMeter.Modules.Users.Core.DAL.Repositories;
using SaveMeter.Modules.Users.Core.DTO;
using SaveMeter.Modules.Users.Core.Entities;
using SaveMeter.Shared.Abstractions.Queries;

namespace SaveMeter.Modules.Users.Core.Queries.Handlers;

internal class GetUserHandler : IQueryHandler<GetUser, UserDetailsDto>
{
    private readonly UserReadRepository _repository;
    private readonly RoleReadRepository _roleRepository;

    public GetUserHandler(UserReadRepository repository, RoleReadRepository roleRepository)
    {
        _repository = repository;
        _roleRepository = roleRepository;
    }

    public async Task<UserDetailsDto> HandleAsync(GetUser query, CancellationToken cancellationToken = default)
    {
        var user =  await _repository.Collection.Aggregate().Match(x => x.Id == query.UserId)
            .Lookup(_roleRepository.Collection, (x => x.RoleIds), (x => x.Id),
                @as: (User eo) => eo.Roles)
            .SingleOrDefaultAsync(cancellationToken: cancellationToken);

        return new UserDetailsDto
        {
            Id = user.Id,
            Email = user.Email,
            Roles = user.Roles.Select(x => x.Name).ToList(),
            Permissions = user.Roles.SelectMany(x => x.Permissions),
            State = user.State.ToString(),
            CreatedAt = user.CreatedAt,
            UpdatedAt = user.UpdatedAt,
        };
    }
}