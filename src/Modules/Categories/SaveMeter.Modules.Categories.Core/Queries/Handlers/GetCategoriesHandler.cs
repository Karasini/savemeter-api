using SaveMeter.Shared.Abstractions.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SaveMeter.Modules.Categories.Core.DTO;

namespace SaveMeter.Modules.Categories.Core.Queries.Handlers;
internal class GetCategoriesHandler : IQueryHandler<GetCategories, List<CategoryDto>>
{
    public Task<List<CategoryDto>> HandleAsync(GetCategories query, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
