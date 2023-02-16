using System;
using System.Collections.Generic;

namespace SaveMeter.Modules.Transactions.Core.DTO;

public record GroupedTransactionsListDto
{
    public List<GroupedTransactions> Transactions { get; init; } = new List<GroupedTransactions>();
        
    public record GroupedTransactions
    {
        public DateTime MonthOfYear { get; init; }
        public decimal Income { get; init; }
        public decimal Outcome { get; init; }
        public List<TransactionsByCategory> TransactionsByCategories { get; init; }
    }

    public record TransactionsByCategory
    {
        public Guid CategoryId { get; init; }
        public string CategoryName { get; init; }
        public decimal Value { get; init; }
    }
}

