using Instapp.Common.Cqrs.Queries;
using SaveMeter.Services.Finances.Application.DTO;
using SaveMeter.Services.Finances.Application.Queries;
using SaveMeter.Services.Finances.Infrastructure.Mongo.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SaveMeter.Services.Finances.Domain.Aggregates.FinancialGoal;
using MongoDB.Driver;
using SaveMeter.Services.Finances.Infrastructure.Mongo;

namespace SaveMeter.Services.Finances.Infrastructure.QueryHandlers
{
    internal class GetFinancialGoalGroupsQueryHandler : IQueryHandler<GetFinancialGoalGroupsQuery, List<FinancialGoalGroupDto>>
    {
        private readonly FinancialGoalGroupReadRepository _repository;

        public GetFinancialGoalGroupsQueryHandler(FinancialGoalGroupReadRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<FinancialGoalGroupDto>> Handle(GetFinancialGoalGroupsQuery request, CancellationToken cancellationToken)
        {
            return await _repository.Find(x => !string.IsNullOrEmpty(x.Title)).ProjectToFinancialGoalGroupDto()
                .SortBy(x => x.CreatedAt).ToListAsync(cancellationToken: cancellationToken);
        }
    }
}
