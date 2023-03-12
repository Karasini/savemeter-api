using System;
using System.Threading.Tasks;
using MongoDB.Driver;
using SaveMeter.Modules.Transactions.Core.Entities;
using SaveMeter.Modules.Transactions.Core.Repositories;
using SaveMeter.Shared.Infrastructure.Mongo.Context;
using SaveMeter.Shared.Infrastructure.Mongo.Repository;

namespace SaveMeter.Modules.Transactions.Core.DAL.Repositories;

public class PredictionModelRepository : BaseRepository<PredictionModel>, IPredictionModelRepository
{
    public PredictionModelRepository(IMongoContext context) : base(context)
    {
    }

    public Task<PredictionModel> GetByUserId(Guid userId)
    {
        return DbCollection.Find(x => x.UserId == userId).FirstOrDefaultAsync();
    }
}