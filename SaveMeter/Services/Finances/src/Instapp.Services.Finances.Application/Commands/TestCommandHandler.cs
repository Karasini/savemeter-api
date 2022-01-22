using Instapp.Common.Cqrs.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Instapp.Services.Finances.Application.Commands
{
    public class TestCommandHandler : ICommandHandler<TestCommand<Guid>, Guid>
    {
        public async Task<Guid> Handle(TestCommand<Guid> request, CancellationToken cancellationToken)
        {
            await Task.Delay(250, cancellationToken);
            return Guid.NewGuid();
        }
    }
}
