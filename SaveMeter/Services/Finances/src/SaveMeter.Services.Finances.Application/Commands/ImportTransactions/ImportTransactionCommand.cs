using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Instapp.Common.Cqrs.Commands;

namespace SaveMeter.Services.Finances.Application.Commands.ImportTransactions
{
    public class ImportTransactionCommand : CommandBase
    {
        public class Transaction
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

        public List<Transaction> Transactions { get; set; }
    }
}
