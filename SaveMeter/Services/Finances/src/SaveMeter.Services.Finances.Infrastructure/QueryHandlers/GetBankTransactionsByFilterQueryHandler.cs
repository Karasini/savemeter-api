using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Instapp.Common.Cqrs.Queries;
using Instapp.Services.Identity.Infrastructure;
using LinqKit;
using MongoDB.Driver;
using SaveMeter.Services.Finances.Application.DTO;
using SaveMeter.Services.Finances.Application.Queries;
using SaveMeter.Services.Finances.Domain.Aggregates.CategoryAggregate;
using SaveMeter.Services.Finances.Domain.Aggregates.Transaction;
using SaveMeter.Services.Finances.Infrastructure.Mongo;
using SaveMeter.Services.Finances.Infrastructure.Mongo.Repositories;
using PredicateBuilder = Instapp.Services.Identity.Infrastructure.PredicateBuilder;

namespace SaveMeter.Services.Finances.Infrastructure.QueryHandlers
{
    internal class GetBankTransactionsByFilterQueryHandler : IQueryHandler<GetBankTransactionsByFilterQuery, PaginatedDto<BankTransactionDto>>
    {
        private readonly BankTransactionReadRepository _repository;
        private readonly CategoryReadRepository _categoryReadRepository;

        public GetBankTransactionsByFilterQueryHandler(BankTransactionReadRepository bankTransactionReadRepository, CategoryReadRepository categoryReadRepository)
        {
            _repository = bankTransactionReadRepository;
            _categoryReadRepository = categoryReadRepository;
        }

        public async Task<PaginatedDto<BankTransactionDto>> Handle(GetBankTransactionsByFilterQuery request, CancellationToken cancellationToken)
        {
            var pageNumber = request.PageNumber ?? 1;
            var pageSize = request.PageSize ?? 100;
            var filter = PredicateBuilder.True<BankTransaction>().
                AndIfAcceptable(request.StartDate.HasValue, x => x.TransactionDate > request.StartDate!.Value)
                .AndIfAcceptable(request.EndDate.HasValue, x => x.TransactionDate < request.EndDate!.Value);

            var count = await _repository.Find(filter.Expand()).CountDocumentsAsync();

            //TODO: Optimize getting count off all documents
            //var countFacet = AggregateFacet.Create("count", PipelineDefinition<BankTransaction, AggregateCountResult>.Create(new[]
            //{
            //    PipelineStageDefinitionBuilder.Count<BankTransaction>()
            //}));

            //var dataFacet = AggregateFacet.Create("dataFacet",
            //PipelineDefinition<BankTransaction, BankTransaction>.Create(new[]
            //{
            //    PipelineStageDefinitionBuilder.Sort(Builders<BankTransaction>.Sort.Ascending(x => x.TransactionDate)),
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

            var bankTransactions = await _repository.Collection.Aggregate()
                .Match(filter.Expand())
                .SortByDescending(x => x.TransactionDate)
                .Skip((pageNumber - 1) * pageSize)
                .Limit(pageSize)
                .Lookup<BankTransaction, Category, BankTransaction>(_categoryReadRepository.Collection, x => x.CategoryId, x => x.Id, x => x.Categories)
                .ProjectToBankTransactionDto()
                .ToListAsync();


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
