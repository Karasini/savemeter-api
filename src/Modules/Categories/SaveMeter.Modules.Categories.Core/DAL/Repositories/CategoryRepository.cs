using System.Threading.Tasks;
using MongoDB.Driver;
using SaveMeter.Modules.Categories.Core.Entities;
using SaveMeter.Modules.Categories.Core.Repositories;
using SaveMeter.Shared.Infrastructure.Mongo.Context;
using SaveMeter.Shared.Infrastructure.Mongo.Repository;

namespace SaveMeter.Modules.Categories.Core.DAL.Repositories
{
    class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(IMongoContext context) : base(context)
        {
        }

        public async Task<Category> GetByCategoryName(string categoryName)
        {
            return await DbCollection.Find(x => x.Name == categoryName).FirstOrDefaultAsync();
        }
    }
}
