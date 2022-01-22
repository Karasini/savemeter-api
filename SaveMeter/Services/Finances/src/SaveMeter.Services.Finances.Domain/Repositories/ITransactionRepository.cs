using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Instapp.Common.MongoDb.Repository;
using SaveMeter.Services.Finances.Domain.Aggregates.Transaction;

namespace SaveMeter.Services.Finances.Domain.Repositories
{
    public interface ITransactionRepository : IRepository<BankTransaction>
    {
        Task<bool> TransactionExists(DateTime transactionDate, decimal value);
    }
}
