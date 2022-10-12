using System;
using System.Threading.Tasks;
using SaveMeter.Modules.Transactions.Core.Entities;
using SaveMeter.Shared.Abstractions.DAL;

namespace SaveMeter.Modules.Transactions.Core.Repositories
{
    public interface IBankTransactionRepository : IRepository<BankTransaction>
    {
        Task<bool> TransactionExists(DateTime transactionDate, decimal value);
    }
}
