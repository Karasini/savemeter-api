using FluentValidation;

namespace SaveMeter.Services.Finances.Application.Commands.CreateFinancialGoal
{
    public class CreateFinancialGoalCommandValidator : AbstractValidator<CreateFinancialGoalCommand>
    {
        public CreateFinancialGoalCommandValidator()
        {
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.Amount).GreaterThanOrEqualTo(0);
        }
    }
}
