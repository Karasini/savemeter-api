using System;
using System.Threading.Tasks;
using SaveMeter.Modules.Transactions.Core.Entities;
using SaveMeter.Modules.Transactions.Core.Repositories;
using SaveMeter.Shared.Infrastructure.Mongo.Context;
using SaveMeter.Shared.Infrastructure.Mongo.Repository;

namespace SaveMeter.Modules.Transactions.Core.DAL.Repositories
{
    class BankTransactionRepository : BaseRepository<BankTransaction>, IBankTransactionRepository
    {
        public BankTransactionRepository(IMongoContext context) : base(context)
        {
        }

        public async Task<bool> TransactionExists(DateTime transactionDate, decimal value)
        {
            return await Exists(x => x.TransactionDate == transactionDate && x.Value == value);
        }
    }
}
