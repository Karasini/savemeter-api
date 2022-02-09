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
    internal class GetCategoriesQueryHandler : IQueryHandler<GetCategoriesQuery, List<CategoryDto>>
    {
        private readonly CategoryReadRepository _categoryReadRepository;

        public GetCategoriesQueryHandler(CategoryReadRepository categoryReadRepository)
        {
            _categoryReadRepository = categoryReadRepository;
        }

        public async Task<List<CategoryDto>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
        {
            return await _categoryReadRepository.Find(x => !string.IsNullOrEmpty(x.Name)).ProjectToCategoryDto().ToListAsync(cancellationToken: cancellationToken);
        }
    }
}
