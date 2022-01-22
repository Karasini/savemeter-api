using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Instapp.Common.MongoDb.Repository;
using Instapp.Services.Finances.Domain.Aggregates.Category;

namespace Instapp.Services.Finances.Domain.Repositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
    }
}
