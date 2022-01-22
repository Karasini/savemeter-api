using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Instapp.Common.MongoDb.Context;
using Instapp.Common.MongoDb.Repository;
using Instapp.Services.Finances.Domain.Aggregates.Transaction;
using Instapp.Services.Finances.Domain.Repositories;

namespace Instapp.Services.Finances.Infrastructure.Mongo.Repositories
{
    class TransactionRepository : BaseRepository<BankTransaction>, ITransactionRepository
    {
        public TransactionRepository(IMongoContext context) : base(context)
        {
        }

        public async Task<bool> TransactionExists(DateTime transactionDate, decimal value)
        {
            return await Exists(x => x.TransactionDate == transactionDate && x.Value == value);
        }
    }
}
