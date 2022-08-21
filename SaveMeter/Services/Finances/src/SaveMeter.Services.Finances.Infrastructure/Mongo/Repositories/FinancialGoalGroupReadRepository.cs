using Instapp.Common.MongoDb.Context;
using Instapp.Common.MongoDb.Repository;
using SaveMeter.Services.Finances.Domain.Aggregates.FinancialGoal;

namespace SaveMeter.Services.Finances.Infrastructure.Mongo.Repositories
{
    internal class FinancialGoalGroupReadRepository : ReadBaseRepository<FinancialGoalGroup>
    {
        public FinancialGoalGroupReadRepository(IMongoContext context) : base(context)
        {
        }
    }
}
