using SaveMeter.Modules.Goals.Core.Entities;
using SaveMeter.Modules.Goals.Core.Repositories;
using SaveMeter.Shared.Infrastructure.Mongo.Context;
using SaveMeter.Shared.Infrastructure.Mongo.Repository;

namespace SaveMeter.Modules.Goals.Core.DAL.Repositories
{
    internal class GoalsGroupRepository : BaseRepository<GoalsGroup>, IGoalsGroupRepository
    {
        public GoalsGroupRepository(IMongoContext context) : base(context)
        {
        }
    }
}
