using Instapp.Common.MongoDb.Repository;
using SaveMeter.Services.Finances.Domain.Aggregates.FinancialGoal;

namespace SaveMeter.Services.Finances.Domain.Repositories
{
    public interface IFinancialGoalGroupRepository : IRepository<FinancialGoalGroup>
    {
    }
}
