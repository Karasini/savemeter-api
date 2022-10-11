using System.Threading.Tasks;
using SaveMeter.Modules.Categories.Core.Entities;
using SaveMeter.Shared.Abstractions.DAL;

namespace SaveMeter.Modules.Categories.Core.Repositories
{
    internal interface ICategoryRepository : IRepository<Category>
    {
        Task<Category> GetByCategoryName(string categoryName);
    }
}
