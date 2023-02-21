using System;
using System.Collections.Generic;

namespace SaveMeter.Modules.Goals.Core.DTO;

internal class GoalsGroupDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public decimal TotalAmount { get; set; }
    public List<GoalDto> Goals { get; set; }
}

internal class GoalDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public decimal Amount { get; set; }
    public string Rank { get; set; }
}