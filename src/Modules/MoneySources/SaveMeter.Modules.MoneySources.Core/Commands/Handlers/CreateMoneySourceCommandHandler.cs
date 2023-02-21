using System.Threading;
using System.Threading.Tasks;
using SaveMeter.Modules.MoneySources.Core.DTO;
using SaveMeter.Modules.MoneySources.Core.Entities;
using SaveMeter.Modules.MoneySources.Core.Repositories;
using SaveMeter.Shared.Abstractions.Commands;

namespace SaveMeter.Modules.MoneySources.Core.Commands.Handlers;

internal class CreateMoneySourceCommandHandler : ICommandHandler<CreateMoneySource, MoneySourceDto>
{
    private readonly IMoneySourceRepository _repository;

    public CreateMoneySourceCommandHandler(IMoneySourceRepository repository)
    {
        _repository = repository;
    }

    public Task<MoneySourceDto> HandleAsync(CreateMoneySource command, CancellationToken cancellationToken = default)
    {
        var moneySource = MoneySource.CreateCustom(command.Title, command.Amount, command.Currency,
            command.NativeAmount, command.NativeCurrency);

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