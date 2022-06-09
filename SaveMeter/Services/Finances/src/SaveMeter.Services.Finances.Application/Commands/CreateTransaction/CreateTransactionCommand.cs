using Instapp.Common.Cqrs.Commands;

namespace SaveMeter.Services.Finances.Application.Commands.CreateTransaction
{
    public class CreateTransactionCommand : CommandBase
    {
        public string? AccountNumber { get; set; }
        public DateTime TransactionDateUtc { get; set; }
        public string RelatedAccountNumber { get; set; }
        public string Customer { get; set; }
        public string Description { get; set; }
        public decimal Value { get; set; }
        public decimal AccountBalance { get; set; }
        public string BankName { get; set; }
    }
}
