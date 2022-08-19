using Instapp.Common.Cqrs.Commands;
using Instapp.Common.Exception.Guards;
using Instapp.Common.Exception.Models;
using SaveMeter.Services.Finances.Application.DTO;
using SaveMeter.Services.Finances.Application.Exceptions;
using SaveMeter.Services.Finances.Domain.Aggregates.MoneySourceAggregate;
using SaveMeter.Services.Finances.Domain.Repositories;

namespace SaveMeter.Services.Finances.Application.Commands.UpdateMoneySource
{
    internal class UpdateMoneySourceCommandHandler : ICommandHandler<UpdateMoneySourceCommand, MoneySourceDto>
    {
        private readonly IMoneySourceRepository _repository;

        public UpdateMoneySourceCommandHandler(IMoneySourceRepository repository)
        {
            _repository = repository;
        }

        public async Task<MoneySourceDto> Handle(UpdateMoneySourceCommand request, CancellationToken cancellationToken)
        {
            var moneySource = await _repository.GetById(request.Id);

            Guard.Against<MoneySourceNotFoundException>(moneySource == null);

            moneySource.Title = request.Title;
            moneySource.Amount = request.Amount;
            moneySource.Currency = request.Currency;
            moneySource.NativeAmount = request.NativeAmount;
            moneySource.NativeCurrency = request.NativeCurrency;
            moneySource.StatusDate = request.StatusDate;

            _repository.Update(moneySource);

            return new MoneySourceDto
            {
                NativeAmount = moneySource.NativeAmount,
                Title = moneySource.Title,
                StatusDate = moneySource.StatusDate,
                Type = moneySource.Type.ToString(),
                NativeCurrency = moneySource.NativeCurrency,
                Currency = moneySource.Currency,
                Amount = moneySource.Amount,
                Id = moneySource.Id
            };

        }
    }
}
