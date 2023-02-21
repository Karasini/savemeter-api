using SaveMeter.Modules.Transactions.Core.Entities;
using SaveMeter.Shared.Infrastructure.Mongo.Context;
using SaveMeter.Shared.Infrastructure.Mongo.Repository;

namespace SaveMeter.Modules.Transactions.Core.DAL.Repositories
{
    internal class BankTransactionReadRepository : ReadBaseRepository<BankTransaction>
    {
        public BankTransactionReadRepository(IMongoContext context) : base(context)
        {
        }
    }
}
