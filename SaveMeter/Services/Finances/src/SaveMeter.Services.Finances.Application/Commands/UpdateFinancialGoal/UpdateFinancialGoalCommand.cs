using Instapp.Common.Cqrs.Commands;
using SaveMeter.Services.Finances.Application.DTO;

namespace SaveMeter.Services.Finances.Application.Commands.UpdateFinancialGoal
{
    public class UpdateFinancialGoalCommand : CommandBase<FinancialGoalGroupDto>
    {
        public Guid Id { get; set; }
        public Guid GoalGroupId { get; set; }
        public string Title { get; set; }
        public decimal Amount { get; set; }
    }
}
