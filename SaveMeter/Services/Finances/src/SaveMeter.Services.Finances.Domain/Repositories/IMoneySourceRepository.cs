using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Instapp.Common.MongoDb.Repository;
using SaveMeter.Services.Finances.Domain.Aggregates.MoneySourceAggregate;

namespace SaveMeter.Services.Finances.Domain.Repositories
{
    public interface IMoneySourceRepository : IRepository<MoneySource>
    {
        Task<MoneySource> GetByType(MoneySourceType type);
    }
}
