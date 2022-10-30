using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;
using SaveMeter.Modules.Transactions.Core.DAL;
using SaveMeter.Modules.Transactions.Core.DAL.Repositories;
using SaveMeter.Modules.Transactions.Core.DTO;
using SaveMeter.Modules.Transactions.Core.Entities;
using SaveMeter.Shared.Abstractions.Filtering;
using SaveMeter.Shared.Abstractions.Queries;
using LinqKit;

namespace SaveMeter.Modules.Transactions.Core.Queries.Handlers
{
    internal class GetBankTransactionsByFilterHandler : IQueryHandler<GetBankTransactionsByFilter, PaginatedDto<BankTransactionDto>>
    {
        private readonly BankTransactionReadRepository _repository;
        private readonly CategoryReadRepository _categoryRepository;

        public GetBankTransactionsByFilterHandler(BankTransactionReadRepository bankTransactionReadRepository, CategoryReadRepository categoryRepository)
        {
            _repository = bankTransactionReadRepository;
            _categoryRepository = categoryRepository;
        }


        public async Task<PaginatedDto<BankTransactionDto>> HandleAsync(GetBankTransactionsByFilter query, CancellationToken cancellationToken = default)
        {
            var pageNumber = query.PageNumber ?? 1;
            var pageSize = query.PageSize ?? 100;
            var filter = FilterBuilder.True<BankTransaction>().
                AndIfAcceptable(query.StartDate.HasValue, x => x.TransactionDate > query.StartDate!.Value)
                .AndIfAcceptable(query.EndDate.HasValue, x => x.TransactionDate < query.EndDate!.Value);

            var count = await _repository.Find(filter.Expand()).CountDocumentsAsync(cancellationToken);

            //TODO: Optimize getting count off all documents
            //var countFacet = AggregateFacet.Create("count", PipelineDefinition<BankTransaction, AggregateCountResult>.Create(new[]
            //{
            //    PipelineStageDefinitionBuilder.Count<BankTransaction>()
            //}));

            //var dataFacet = AggregateFacet.Create("dataFacet",
            //PipelineDefinition<BankTransaction, BankTransaction>.Create(new[]
            //{
            //    PipelineStageDefinitionBuilder.Sort(Builders<BankTransaction>.Sort.Ascending(x => x.TransactionDateUtc)),
            //    PipelineStageDefinitionBuilder.Skip<BankTransaction>((pageNumber - 1) * pageSize),
            //    PipelineStageDefinitionBuilder.Limit<BankTransaction>(pageSize),
            //}));

            //var lookupFacet = AggregateFacet.Create("lookupFacet",
            //PipelineDefinition<BankTransaction, BankTransactionDto>.Create(new[]
            //{
            //    PipelineStageDefinitionBuilder.Lookup<BankTransaction, Category, BankTransaction>(_categoryReadRepository.Collection, x => x.CategoryId, x => x.Id, x => x.Categories).,
            //    PipelineStageDefinitionBuilder.Project(BankTransactionProjections.Projection),
            //}));

            //var projectFacet = AggregateFacet.Create("projectFacet",
            //PipelineDefinition<BankTransaction, BankTransactionDto>.Create(new[]
            //{
            //    PipelineStageDefinitionBuilder.Project(BankTransactionProjections.Projection),
            //}));

            //var aggregation = await _repository.Collection.Aggregate()
            //    .Match(filter.Expand())
            //    .Facet(countFacet, dataFacet, lookupFacet, projectFacet)
            //    .ToListAsync();

            //var count = aggregation.First()
            //    .Facets.First(x => x.Name == "count")
            //    .Output<AggregateCountResult>()
            //    ?.FirstOrDefault()
            //    ?.Count ?? 0;

            //var data = aggregation.First()
            //    .Facets.First(x => x.Name == "projectFacet")
            //    .Output<BankTransactionDto>();
            // .Unwind("items.vendor", new AggregateUnwindOptions<VendorDetail>() { PreserveNullAndEmptyArrays = true })

            var bankTransactions = await _repository.Collection.Aggregate()
                .Match(filter.Expand())
                .Lookup(_categoryRepository.Collection, (x => x.CategoryId), 
                    (x => x.Id), @as: (BankTransaction t) => t.Category)
                .Unwind(x => x.Category, new AggregateUnwindOptions<BankTransaction>(){PreserveNullAndEmptyArrays = true})
                .SortByDescending(x => x.TransactionDate)
                .Skip((pageNumber - 1) * pageSize)
                .Limit(pageSize)
                .ProjectToBankTransactionDto()
                .ToListAsync(cancellationToken: cancellationToken);
            
            return new PaginatedDto<BankTransactionDto>
            {
                CurrentPage = pageNumber,
                Items = bankTransactions.ToList(),
                PageSize = pageSize,
                TotalCount = count,
                TotalPages = (int)Math.Ceiling(count / (double)pageSize)
            };
        }
    }
}
