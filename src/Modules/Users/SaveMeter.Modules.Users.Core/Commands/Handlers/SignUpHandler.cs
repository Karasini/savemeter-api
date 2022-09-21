using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SaveMeter.Shared.Abstractions.Commands;

namespace SaveMeter.Modules.Users.Core.Commands.Handlers;
internal sealed class SignUpHandler : ICommandHandler<SignUp>
{
    public Task HandleAsync(SignUp command, CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }
}
