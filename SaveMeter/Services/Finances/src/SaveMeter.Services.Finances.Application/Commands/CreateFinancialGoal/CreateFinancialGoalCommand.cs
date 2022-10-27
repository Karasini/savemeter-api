﻿using Instapp.Common.Cqrs.Commands;
using SaveMeter.Services.Finances.Application.DTO;

namespace SaveMeter.Services.Finances.Application.Commands.CreateFinancialGoal
{
    public class CreateFinancialGoalCommand : CommandBase<FinancialGoalGroupDto>
    {
        public Guid GoalGroupId { get; set; }
        public string Title { get; set; }
        public decimal Amount { get; set; }
        public string Rank { get; set; }
    }
}
