using System.Threading;
using System.Threading.Tasks;
using SaveMeter.Modules.MoneySources.Core.DTO;
using SaveMeter.Modules.MoneySources.Core.Exceptions;
using SaveMeter.Modules.MoneySources.Core.Repositories;
using SaveMeter.Shared.Abstractions.Commands;
using SaveMeter.Shared.Abstractions.Guards;

namespace SaveMeter.Modules.MoneySources.Core.Commands.Handlers;

internal class UpdateMoneySourceCommandHandler : ICommandHandler<UpdateMoneySource, MoneySourceDto>
{
    private readonly IMoneySourceRepository _repository;

    public UpdateMoneySourceCommandHandler(IMoneySourceRepository repository)
    {
        _repository = repository;
    }

    public async Task<MoneySourceDto> HandleAsync(UpdateMoneySource command, CancellationToken cancellationToken = default)
    {
        var moneySource = await _repository.GetByIdAsync(command.Id);

        Guard.Against<MoneySourceNotFoundException>(moneySource == null);

        moneySource.Title = command.Title;
        moneySource.Amount = command.Amount;
        moneySource.Currency = command.Currency;
        moneySource.NativeAmount = command.NativeAmount;
        moneySource.NativeCurrency = command.NativeCurrency;
        moneySource.StatusDate = command.StatusDate;

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