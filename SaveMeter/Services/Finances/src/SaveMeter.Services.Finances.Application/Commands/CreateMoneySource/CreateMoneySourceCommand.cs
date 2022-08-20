using Instapp.Common.Cqrs.Commands;
using SaveMeter.Services.Finances.Application.DTO;
using SaveMeter.Services.Finances.Domain.Aggregates.MoneySourceAggregate;

namespace SaveMeter.Services.Finances.Application.Commands.CreateMoneySource
{
    public class CreateMoneySourceCommand : CommandBase<MoneySourceDto>
    {
        public string Title { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public decimal NativeAmount { get; set; }
        public string NativeCurrency { get; set; }
        public DateTime StatusDate { get; set; }
    }
}
