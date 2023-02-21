using System;
using System.Collections.Generic;

namespace SaveMeter.Modules.Transactions.Core.DTO;

public record GroupedTransactionsListDto
{
    public List<GroupedTransactions> Transactions { get; init; } = new List<GroupedTransactions>();

    public record GroupedTransactions
    {
        public int Year { get; init; }
        public int Month { get; init; }
        public decimal Income { get; init; }
        public decimal Outcome { get; init; }
        public List<TransactionsByCategory> Transactions { get; init; }
    }

    public record TransactionsByCategory
    {
        public Guid CategoryId { get; init; }
        public string CategoryName { get; init; }
        public decimal Value { get; init; }
    }
}