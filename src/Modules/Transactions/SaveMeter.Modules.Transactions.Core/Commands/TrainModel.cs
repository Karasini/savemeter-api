using System;
using SaveMeter.Shared.Abstractions.Commands;

namespace SaveMeter.Modules.Transactions.Core.Commands;

internal class TrainModel : ICommand
{
    public Guid UserId { get; set; }
}