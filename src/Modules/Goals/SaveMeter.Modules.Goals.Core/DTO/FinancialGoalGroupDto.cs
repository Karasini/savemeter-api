using System;
using System.Collections.Generic;

namespace SaveMeter.Modules.Goals.Core.DTO;

internal class FinancialGoalGroupDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public decimal TotalAmount { get; set; }
    public List<FinancialGoalDto> Goals { get; set; }
}

internal class FinancialGoalDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public decimal Amount { get; set; }
    public string Rank { get; set; }
}