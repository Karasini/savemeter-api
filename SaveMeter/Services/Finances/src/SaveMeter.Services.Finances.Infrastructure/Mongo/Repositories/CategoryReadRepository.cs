using Instapp.Common.MongoDb.Context;
using Instapp.Common.MongoDb.Repository;
using SaveMeter.Services.Finances.Domain.Aggregates.CategoryAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaveMeter.Services.Finances.Infrastructure.Mongo.Repositories
{
    internal class CategoryReadRepository : ReadBaseRepository<Category>
    {
        public CategoryReadRepository(IMongoContext context) : base(context)
        {
        }
    }
}
