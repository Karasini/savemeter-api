using FluentValidation;

namespace SaveMeter.Services.Finances.Application.Commands.UpdateFinancialGoal
{
    public class UpdateFinancialGoalCommandValidator : AbstractValidator<UpdateFinancialGoalCommand>
    {
        public UpdateFinancialGoalCommandValidator()
        {
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.Amount).GreaterThanOrEqualTo(0);
        }
    }
}
