using Instapp.Common.Cqrs.Commands;
using SaveMeter.Services.Finances.Application.DTO;

namespace SaveMeter.Services.Finances.Application.Commands.DeleteFinancialGoal
{
    public class DeleteFinancialGoalCommand : CommandBase<FinancialGoalGroupDto>
    {
        public Guid Id { get; set; }
        public Guid GoalGroupId { get; set; }
    }
}
