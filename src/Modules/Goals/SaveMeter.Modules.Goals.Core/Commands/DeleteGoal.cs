using System;
using SaveMeter.Modules.Goals.Core.DTO;
using SaveMeter.Shared.Abstractions.Commands;

namespace SaveMeter.Modules.Goals.Core.Commands;

internal class DeleteGoal : ICommand<GoalsGroupDto>
{
    public Guid Id { get; set; }
    public Guid GoalGroupId { get; set; }
}