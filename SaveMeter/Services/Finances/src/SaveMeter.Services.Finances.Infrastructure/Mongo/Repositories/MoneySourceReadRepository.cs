using Instapp.Common.MongoDb.Context;
using Instapp.Common.MongoDb.Repository;
using SaveMeter.Services.Finances.Domain.Aggregates.CategoryAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SaveMeter.Services.Finances.Domain.Aggregates.MoneySourceAggregate;

namespace SaveMeter.Services.Finances.Infrastructure.Mongo.Repositories
{
    internal class MoneySourceReadRepository : ReadBaseRepository<MoneySource>
    {
        public MoneySourceReadRepository(IMongoContext context) : base(context)
        {
        }
    }
}
