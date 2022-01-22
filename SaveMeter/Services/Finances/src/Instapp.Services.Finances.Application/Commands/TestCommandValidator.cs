using FluentValidation;
using System;

namespace Instapp.Services.Finances.Application.Commands
{
    public class TestCommandValidator : AbstractValidator<TestCommand<Guid>>
    {
        public TestCommandValidator()
        {
            RuleFor(c => c.TestProperty).NotEmpty();
        }
    }
}
