using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace SaveMeter.Services.Finances.Application.Commands.CreateMoneySource
{
    public class CreateMoneySourceCommandValidator : AbstractValidator<CreateMoneySourceCommand>
    {
        public CreateMoneySourceCommandValidator()
        {
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.Amount).GreaterThanOrEqualTo(0);
            RuleFor(x => x.Currency).NotEmpty();
        }
    }
}
