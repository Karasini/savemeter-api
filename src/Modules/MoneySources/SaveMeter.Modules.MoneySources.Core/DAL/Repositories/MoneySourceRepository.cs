using System.Threading.Tasks;
using MongoDB.Driver;
using SaveMeter.Modules.MoneySources.Core.Entities;
using SaveMeter.Modules.MoneySources.Core.Repositories;
using SaveMeter.Services.Finances.Domain.Aggregates.MoneySourceAggregate;
using SaveMeter.Shared.Infrastructure.Mongo.Context;
using SaveMeter.Shared.Infrastructure.Mongo.Repository;

namespace SaveMeter.Modules.MoneySources.Core.DAL.Repositories;

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