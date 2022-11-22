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
    internal class GetGoalsGroupsQueryHandler : IQueryHandler<GetGoalsGroups, List<GoalsGroupDto>>
    {
        private readonly GoalsGroupReadRepository _repository;

        public GetGoalsGroupsQueryHandler(GoalsGroupReadRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<GoalsGroupDto>> HandleAsync(GetGoalsGroups query, CancellationToken cancellationToken = default)
        {
            return await _repository.Find(x => !string.IsNullOrEmpty(x.Title)).ProjectToGoalsGroupDto()
                .SortBy(x => x.CreatedAt).ToListAsync(cancellationToken: cancellationToken);
        }
    }
}
