using FluentValidation;

namespace SaveMeter.Services.Finances.Application.Commands.CreateFinancialGoalGroup
{
    public class CreateFinancialGoalGroupCommandValidator : AbstractValidator<CreateFinancialGoalGroupCommand>
    {
        public CreateFinancialGoalGroupCommandValidator()
        {
            RuleFor(x => x.Title).NotEmpty();
        }
    }
}
