using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SaveMeter.Shared.Abstractions.Commands;
using SaveMeter.Shared.Abstractions.Events;

namespace SaveMeter.Modules.Transactions.Core.Commands;
internal record ImportTransactions : ICommand
{
    public record Transaction
    {
        public DateTime TransactionDateUtc { get; init; }
        public string RelatedAccountNumber { get; init; }
        public string Customer { get; init; }
        public string Description { get; init; }
        public decimal Value { get; init; }
        public decimal AccountBalance { get; init; }
        public string BankName { get; init; }
        public Guid UserId { get; init; }
    }

    public List<Transaction> Transactions { get; init; }
}
