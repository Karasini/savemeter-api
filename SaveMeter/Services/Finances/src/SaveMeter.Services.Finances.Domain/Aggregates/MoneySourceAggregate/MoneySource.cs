using Instapp.Common.MongoDb.Models;

namespace SaveMeter.Services.Finances.Domain.Aggregates.MoneySourceAggregate
{
    public class MoneySource : Entity, IAggregateRoot
    {
        public string Title { get; set; }
        public MoneySourceType Type { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public decimal NativeAmount { get; set; }
        public string NativeCurrency { get; set; }
        public DateTime StatusDate { get; set; }
        public string Icon { get; set; }

        public void UpdateNativeAmount(decimal cost)
        {
            NativeAmount += cost;
            StatusDate = DateTime.UtcNow;
        }

        public static MoneySource CreateCustom(string title, decimal amount, string currency, decimal nativeAmount,
            string nativeCurrency) =>
            new MoneySource()
            {
                Title = title,
                Type = MoneySourceType.Custom,
                Amount = amount,
                Currency = currency,
                NativeAmount = nativeAmount,
                NativeCurrency = nativeCurrency,
                StatusDate = DateTime.UtcNow,
            };
    }
}
