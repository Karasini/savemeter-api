using SaveMeter.Shared.Abstractions.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;
using SaveMeter.Modules.Categories.Core.DAL;
using SaveMeter.Modules.Categories.Core.DTO;
using SaveMeter.Modules.Categories.Core.DAL.Repositories;

namespace SaveMeter.Modules.Categories.Core.Queries.Handlers;
internal class GetCategoriesHandler : IQueryHandler<GetCategories, List<CategoryDto>>
{
    private readonly CategoryReadRepository _repository;

    public GetCategoriesHandler(CategoryReadRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<CategoryDto>> HandleAsync(GetCategories query, CancellationToken cancellationToken = default)
    {
        return await _repository
            .Find(x => !string.IsNullOrEmpty(x.Name))
            .ProjectToCategoryDto()
            .SortBy(x => x.Name)
            .ToListAsync(cancellationToken: cancellationToken);
    }
}
