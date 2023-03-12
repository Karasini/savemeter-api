using System;
using System.Threading.Tasks;
using SaveMeter.Modules.Transactions.Core.Entities;
using SaveMeter.Shared.Abstractions.DAL;

namespace SaveMeter.Modules.Transactions.Core.Repositories;

public interface IPredictionModelRepository : IRepository<PredictionModel>
{
    public Task<PredictionModel> GetByUserId(Guid userId);
}