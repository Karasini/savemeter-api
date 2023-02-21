using SaveMeter.Modules.MoneySources.Core.Entities;
using SaveMeter.Shared.Infrastructure.Mongo.Context;
using SaveMeter.Shared.Infrastructure.Mongo.Repository;

namespace SaveMeter.Modules.MoneySources.Core.DAL.Repositories;

internal class MoneySourceReadRepository : ReadBaseRepository<MoneySource>
{
    public MoneySourceReadRepository(IMongoContext context) : base(context)
    {
    }
}