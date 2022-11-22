using SaveMeter.Modules.Goals.Core.Entities;
using SaveMeter.Shared.Infrastructure.Mongo.Context;
using SaveMeter.Shared.Infrastructure.Mongo.Repository;

namespace SaveMeter.Modules.Goals.Core.DAL.Repositories
{
    internal class GoalsGroupReadRepository : ReadBaseRepository<GoalsGroup>
    {
        public GoalsGroupReadRepository(IMongoContext context) : base(context)
        {
        }
    }
}
