using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Instapp.Common.Cqrs.Commands;
using Instapp.Common.Exception.Guards;
using SaveMeter.Services.Finances.Application.DTO;
using SaveMeter.Services.Finances.Domain.Aggregates.MoneySourceAggregate;
using SaveMeter.Services.Finances.Domain.Repositories;

namespace SaveMeter.Services.Finances.Application.Commands.CreateMoneySource
{
    internal class CreateMoneySourceCommandHandler : ICommandHandler<CreateMoneySourceCommand, MoneySourceDto>
    {
        private readonly IMoneySourceRepository _repository;

        public CreateMoneySourceCommandHandler(IMoneySourceRepository repository)
        {
            _repository = repository;
        }

        public Task<MoneySourceDto> Handle(CreateMoneySourceCommand request, CancellationToken cancellationToken)
        {
            var moneySource = MoneySource.CreateCustom(request.Title, request.Amount, request.Currency,
                request.NativeAmount, request.NativeCurrency);

            _repository.Add(moneySource);

            return Task.FromResult(new MoneySourceDto
            {
                NativeAmount = moneySource.NativeAmount,
                Title = moneySource.Title,
                StatusDate = moneySource.StatusDate,
                Type = moneySource.Type.ToString(),
                NativeCurrency = moneySource.NativeCurrency,
                Currency = moneySource.Currency,
                Amount = moneySource.Amount,
                Id = moneySource.Id
            });

        }
    }
}
