using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Instapp.Common.MongoDb.Context;
using Instapp.Common.MongoDb.Repository;
using MongoDB.Driver;
using SaveMeter.Services.Finances.Domain.Aggregates.FinancialGoal;
using SaveMeter.Services.Finances.Domain.Aggregates.MoneySourceAggregate;
using SaveMeter.Services.Finances.Domain.Repositories;

namespace SaveMeter.Services.Finances.Infrastructure.Mongo.Repositories
{
    internal class FinancialGoalGroupRepository : BaseRepository<FinancialGoalGroup>, IFinancialGoalGroupRepository
    {
        public FinancialGoalGroupRepository(IMongoContext context) : base(context)
        {
        }
    }
}
