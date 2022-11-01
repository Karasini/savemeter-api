using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;
using SaveMeter.Modules.Goals.Core.DAL;
using SaveMeter.Modules.Goals.Core.DAL.Repositories;
using SaveMeter.Modules.Goals.Core.DTO;
using SaveMeter.Shared.Abstractions.Queries;

namespace SaveMeter.Modules.Goals.Core.Queries.Handlers
{
    internal class GetFinancialGoalGroupsQueryHandler : IQueryHandler<GetFinancialGoalGroups, List<FinancialGoalGroupDto>>
    {
        private readonly FinancialGoalGroupReadRepository _repository;

        public GetFinancialGoalGroupsQueryHandler(FinancialGoalGroupReadRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<FinancialGoalGroupDto>> HandleAsync(GetFinancialGoalGroups query, CancellationToken cancellationToken = default)
        {
            return await _repository.Find(x => !string.IsNullOrEmpty(x.Title)).ProjectToFinancialGoalGroupDto()
                .SortBy(x => x.CreatedAt).ToListAsync(cancellationToken: cancellationToken);
        }
    }
}
