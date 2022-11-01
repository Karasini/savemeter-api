using System.Threading.Tasks;
using SaveMeter.Modules.MoneySources.Core.Entities;
using SaveMeter.Shared.Abstractions.DAL;

namespace SaveMeter.Modules.MoneySources.Core.Repositories;

internal interface IMoneySourceRepository : IRepository<MoneySource>
{
}