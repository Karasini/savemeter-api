using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Instapp.Common.MongoDb.Models;

namespace Instapp.Services.Finances.Domain.Aggregates.Transaction
{
    public class BankTransaction : Entity, IAggregateRoot
    {
        public DateTime TransactionDate { get; set; }
        public string Customer { get; set; }
        public string Description { get; set; }
        public decimal Value { get; set; }
        public Guid? CategoryId { get; set; }
        public bool SkipAnalysis { get; set; }
    }
}
