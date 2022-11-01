using System;
using SaveMeter.Modules.MoneySources.Core.DTO;
using SaveMeter.Shared.Abstractions.Commands;

namespace SaveMeter.Modules.MoneySources.Core.Commands;

internal class UpdateMoneySource : ICommand<MoneySourceDto>
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public decimal Amount { get; set; }
    public string Currency { get; set; }
    public decimal NativeAmount { get; set; }
    public string NativeCurrency { get; set; }
    public DateTime StatusDate { get; set; }
}