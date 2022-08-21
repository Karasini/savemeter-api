using Instapp.Common.Cqrs.Queries;
using MongoDB.Driver;
using SaveMeter.Services.Finances.Application.DTO;
using SaveMeter.Services.Finances.Application.Queries;
using SaveMeter.Services.Finances.Domain.Aggregates.CategoryAggregate;
using SaveMeter.Services.Finances.Infrastructure.Mongo;
using SaveMeter.Services.Finances.Infrastructure.Mongo.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaveMeter.Services.Finances.Infrastructure.QueryHandlers
{
    internal class GetMoneySourcesQueryHandler : IQueryHandler<GetMoneySourcesQuery, List<MoneySourceDto>>
    {
        private readonly MoneySourceReadRepository _moneySourceReadRepository;

        public GetMoneySourcesQueryHandler(MoneySourceReadRepository moneySourceReadRepository)
        {
            _moneySourceReadRepository = moneySourceReadRepository;
        }

        public async Task<List<MoneySourceDto>> Handle(GetMoneySourcesQuery request, CancellationToken cancellationToken)
        {
            return await _moneySourceReadRepository.Find(x => !string.IsNullOrEmpty(x.Title)).ProjectToMoneySourceDto().SortByDescending(x => x.NativeAmount).ToListAsync(cancellationToken: cancellationToken);
        }
    }
}
