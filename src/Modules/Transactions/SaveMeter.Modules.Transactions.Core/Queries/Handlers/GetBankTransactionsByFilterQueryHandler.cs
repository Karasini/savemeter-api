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
                AndIfAcceptable(query.StartDate.HasValue, x => x.TransactionDate >= query.StartDate!.Value)
                .AndIfAcceptable(query.EndDate.HasValue, x => x.TransactionDate < query.EndDate!.Value.AddDays(1))
                .And(x => x.UserId == query.UserId);
            
            var countFacet = AggregateFacet.Create("count",
                PipelineDefinition<BankTransaction, AggregateCountResult>.Create(new[]
                {
                    PipelineStageDefinitionBuilder.Count<BankTransaction>()
                }));

            var dataFacet = AggregateFacet.Create("data",
                PipelineDefinition<BankTransaction, BankTransaction>.Create(new IPipelineStageDefinition[]
                {
                    PipelineStageDefinitionBuilder.Skip<BankTransaction>((pageNumber - 1) * pageSize),
                    PipelineStageDefinitionBuilder.Limit<BankTransaction>(pageSize),
                }));

            var aggregation = _repository.Collection
                .Aggregate()
                .Match(filter.Expand())
                .Lookup(_categoryRepository.Collection, (x => x.CategoryId),
                    (x => x.Id), @as: (BankTransaction t) => t.Category)
                .Unwind(x => x.Category,
                    new AggregateUnwindOptions<BankTransaction>() { PreserveNullAndEmptyArrays = true })
                .SortByDescending(x => x.TransactionDate)
                .Facet(countFacet, dataFacet);

            var result = await aggregation.FirstAsync(cancellationToken: cancellationToken);
            
            var count = result
                .Facets.First(x => x.Name == "count")
                .Output<AggregateCountResult>()
                .FirstOrDefault()!
                .Count;
            
            var data = result
                .Facets.First(x => x.Name == "data")
                .Output<BankTransaction>();

            return new PaginatedDto<BankTransactionDto>
            {
                CurrentPage = pageNumber,
                Items = data.Select(x => new BankTransactionDto
                {
                    Id = x.Id,
                    TransactionDate = x.TransactionDate,
                    Customer = x.Customer,
                    Description = x.Description,
                    Value = x.Value,
                    CategoryId = x.CategoryId,
                    CategoryName = x.Category.Name
                }).ToArray(),
                PageSize = pageSize,
                TotalCount = count,
                TotalPages = (int)Math.Ceiling(count / (double)pageSize)
            };
        }
    }
}
