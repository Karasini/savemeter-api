using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Instapp.Common.MongoDb.Context;
using Instapp.Common.MongoDb.Repository;
using MongoDB.Driver;
using SaveMeter.Services.Finances.Domain.Aggregates.MoneySourceAggregate;
using SaveMeter.Services.Finances.Domain.Repositories;

namespace SaveMeter.Services.Finances.Infrastructure.Mongo.Repositories
{
    internal class MoneySourceRepository: BaseRepository<MoneySource>, IMoneySourceRepository
    {
        public MoneySourceRepository(IMongoContext context) : base(context)
        {
        }

        public async Task<MoneySource> GetByType(MoneySourceType type)
        {
            return await DbCollection.Find(x => x.Type == type).FirstOrDefaultAsync();
        }
    }
}
