using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Instapp.Common.MongoDb.Repository;
using SaveMeter.Services.Finances.Domain.Aggregates.CategoryAggregate;

namespace SaveMeter.Services.Finances.Domain.Repositories
{
    public interface ICategoryReferenceRepository : IRepository<CategoryReference>
    {
        Task<CategoryReference> GetIfExistsIn(string description);
    }
}
