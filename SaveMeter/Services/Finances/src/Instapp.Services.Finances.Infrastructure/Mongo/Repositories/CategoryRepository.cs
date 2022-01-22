using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Instapp.Common.MongoDb.Context;
using Instapp.Common.MongoDb.Repository;
using Instapp.Services.Finances.Domain.Aggregates.Category;
using Instapp.Services.Finances.Domain.Repositories;

namespace Instapp.Services.Finances.Infrastructure.Mongo.Repositories
{
    class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(IMongoContext context) : base(context)
        {
        }
    }
}
