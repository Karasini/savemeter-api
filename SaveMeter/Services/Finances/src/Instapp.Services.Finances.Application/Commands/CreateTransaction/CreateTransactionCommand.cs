using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Instapp.Common.Cqrs.Commands;

namespace Instapp.Services.Finances.Application.Commands.CreateTransaction
{
    public class CreateTransactionCommand : CommandBase
    {
        public string AccountNumber { get; set; }
        public DateTime TransactionDate { get; set; }
        public DateTime SettlementDate { get; set; }
        public string RelatedAccountNumber { get; set; }
        public string Customer { get; set; }
        public string Description { get; set; }
        public decimal Value { get; set; }
        public decimal AccountBalance { get; set; }
    }
}
