using Instapp.Common.Cqrs.Commands;
using SaveMeter.Services.Finances.Application.DTO;

namespace SaveMeter.Services.Finances.Application.Commands.UpdateMoneySource
{
    public class UpdateMoneySourceCommand : CommandBase<MoneySourceDto>
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public decimal NativeAmount { get; set; }
        public string NativeCurrency { get; set; }
        public DateTime StatusDate { get; set; }
    }
}
