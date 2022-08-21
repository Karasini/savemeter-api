using FluentValidation;

namespace SaveMeter.Services.Finances.Application.Commands.AddFinancialGoal
{
    public class AddFinancialGoalCommandValidator : AbstractValidator<AddFinancialGoalCommand>
    {
        public AddFinancialGoalCommandValidator()
        {
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.Amount).GreaterThanOrEqualTo(0);
        }
    }
}
