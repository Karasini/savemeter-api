using Instapp.Common.Cqrs.Commands;

namespace SaveMeter.Services.Finances.Application.Commands
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
