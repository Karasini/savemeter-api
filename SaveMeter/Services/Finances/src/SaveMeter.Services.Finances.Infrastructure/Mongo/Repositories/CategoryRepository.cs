using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Instapp.Common.MongoDb.Context;
using Instapp.Common.MongoDb.Repository;
using MongoDB.Driver;
using SaveMeter.Services.Finances.Domain.Aggregates.CategoryAggregate;
using SaveMeter.Services.Finances.Domain.Repositories;

namespace SaveMeter.Services.Finances.Infrastructure.Mongo.Repositories
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
