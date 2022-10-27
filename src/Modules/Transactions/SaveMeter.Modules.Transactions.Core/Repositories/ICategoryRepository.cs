using System.Threading.Tasks;
using SaveMeter.Modules.Transactions.Core.Entities;
using SaveMeter.Shared.Abstractions.DAL;

namespace SaveMeter.Modules.Transactions.Core.Repositories
{
    internal interface ICategoryRepository : IRepository<Category>
    {
        Task<Category> GetByCategoryName(string categoryName);
    }
}
