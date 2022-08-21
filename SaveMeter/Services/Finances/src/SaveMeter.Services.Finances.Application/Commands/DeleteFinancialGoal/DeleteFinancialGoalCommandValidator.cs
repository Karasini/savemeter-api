using FluentValidation;

namespace SaveMeter.Services.Finances.Application.Commands.DeleteFinancialGoal
{
    public class DeleteFinancialGoalCommandValidator : AbstractValidator<DeleteFinancialGoalCommand>
    {
        public DeleteFinancialGoalCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.GoalGroupId).NotEmpty();
        }
    }
}
