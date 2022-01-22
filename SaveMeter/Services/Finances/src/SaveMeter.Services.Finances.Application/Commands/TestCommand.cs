using Instapp.Common.Cqrs.Commands;

namespace SaveMeter.Services.Finances.Application.Commands
{
    public class TestCommand<Guid> : CommandBase<Guid>
    {
        public string? TestProperty { get; set; }
    }
}
