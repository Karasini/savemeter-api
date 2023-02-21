using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;
using SaveMeter.Modules.Transactions.Core.DAL;
using SaveMeter.Modules.Transactions.Core.DAL.Repositories;
using SaveMeter.Modules.Transactions.Core.DTO;
using SaveMeter.Shared.Abstractions.Queries;

namespace SaveMeter.Modules.Transactions.Core.Queries.Handlers;
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
