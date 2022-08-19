using FluentValidation;

namespace SaveMeter.Services.Finances.Application.Commands.UpdateMoneySource
{
    public class UpdateMoneySourceCommandValidator : AbstractValidator<UpdateMoneySourceCommand>
    {
        public UpdateMoneySourceCommandValidator()
        {
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.Amount).GreaterThanOrEqualTo(0);
            RuleFor(x => x.Currency).NotEmpty();
        }
    }
}
