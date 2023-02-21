using System;
using SaveMeter.Modules.Goals.Core.DTO;
using SaveMeter.Shared.Abstractions.Commands;

namespace SaveMeter.Modules.Goals.Core.Commands;

internal class CreateGoal : ICommand<GoalsGroupDto>
{
    public Guid GoalGroupId { get; set; }
    public string Title { get; set; }
    public decimal Amount { get; set; }
    public string Rank { get; set; }
}