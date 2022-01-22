using Instapp.Common.Cqrs.Commands;

namespace SaveMeter.Services.Finances.Application.Commands.TrainNetwork
{
    public class TrainNetworkCommand : CommandBase<string>
    {
        public string? Customer { get; set; }
        public string? Description { get; set; }
    }
}
