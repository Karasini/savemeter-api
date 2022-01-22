using Instapp.Common.MongoDb.Context;
using Instapp.Common.MongoDb.Repository;
using SaveMeter.Services.Finances.Domain.Aggregates.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaveMeter.Services.Finances.Infrastructure.Mongo.Repositories
{
    internal class BankTransactionReadRepository : ReadBaseRepository<BankTransaction>
    {
        public BankTransactionReadRepository(IMongoContext context) : base(context)
        {
        }
    }
}
