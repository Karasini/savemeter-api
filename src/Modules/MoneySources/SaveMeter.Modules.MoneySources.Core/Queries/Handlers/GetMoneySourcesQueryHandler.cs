using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;
using SaveMeter.Modules.MoneySources.Core.DAL;
using SaveMeter.Modules.MoneySources.Core.DAL.Repositories;
using SaveMeter.Modules.MoneySources.Core.DTO;
using SaveMeter.Shared.Abstractions.Queries;

namespace SaveMeter.Modules.MoneySources.Core.Queries.Handlers;

internal class GetMoneySourcesQueryHandler : IQueryHandler<GetMoneySources, List<MoneySourceDto>>
{
    private readonly MoneySourceReadRepository _moneySourceReadRepository;

    public GetMoneySourcesQueryHandler(MoneySourceReadRepository moneySourceReadRepository)
    {
        _moneySourceReadRepository = moneySourceReadRepository;
    }
        
    public async Task<List<MoneySourceDto>> HandleAsync(GetMoneySources query, CancellationToken cancellationToken = default)
    {
        return await _moneySourceReadRepository.Find(x => !string.IsNullOrEmpty(x.Title))
            .ProjectToMoneySourceDto()
            .SortByDescending(x => x.NativeAmount)
            .ToListAsync(cancellationToken: cancellationToken);
    }
}