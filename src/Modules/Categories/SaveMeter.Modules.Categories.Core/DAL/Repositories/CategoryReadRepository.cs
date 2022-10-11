using SaveMeter.Modules.Categories.Core.Entities;
using SaveMeter.Shared.Infrastructure.Mongo.Context;
using SaveMeter.Shared.Infrastructure.Mongo.Repository;

namespace SaveMeter.Modules.Categories.Core.DAL.Repositories
{
    internal class CategoryReadRepository : ReadBaseRepository<Category>
    {
        public CategoryReadRepository(IMongoContext context) : base(context)
        {
        }
    }
}
