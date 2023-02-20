using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LinqKit;
using MongoDB.Driver;
using SaveMeter.Modules.Transactions.Core.DAL.Repositories;
using SaveMeter.Modules.Transactions.Core.DTO;
using SaveMeter.Modules.Transactions.Core.Entities;
using SaveMeter.Shared.Abstractions.Filtering;
using SaveMeter.Shared.Abstractions.Queries;

namespace SaveMeter.Modules.Transactions.Core.Queries.Handlers;

internal class GetGroupedBankTransactionsHandler : IQueryHandler<GetGroupedBankTransactions, GroupedTransactionsListDto>
{
    private readonly BankTransactionReadRepository _transactionRepository;
    private readonly CategoryReadRepository _categoryRepository;

    public GetGroupedBankTransactionsHandler(BankTransactionReadRepository transactionRepository,
        CategoryReadRepository categoryRepository)
    {
        _transactionRepository = transactionRepository;
        _categoryRepository = categoryRepository;
    }

    public async Task<GroupedTransactionsListDto> HandleAsync(GetGroupedBankTransactions query,
        CancellationToken cancellationToken = default)
    {
        var filter = FilterBuilder.True<BankTransaction>()
            .AndIfAcceptable(query.StartDate.HasValue, x => x.TransactionDate >= query.StartDate!.Value)
            .AndIfAcceptable(query.EndDate.HasValue, x => x.TransactionDate < query.EndDate!.Value.AddDays(1))
            .And(x => x.UserId == query.UserId);

        var categories = await _categoryRepository.Collection.Aggregate()
            .ToListAsync(cancellationToken: cancellationToken);

        var result = await _transactionRepository.Collection
            .Aggregate()
            .Match(filter.Expand())
            .Group(x => new
            {
                x.TransactionDate.Year,
                x.TransactionDate.Month,
                x.CategoryId
            }, group => new
            {
                group.Key.Month,
                group.Key.Year,
                group.Key.CategoryId,
                Income = group.Where(x => x.CategoryId == Category.IncomeId).Sum(x => x.Value),
                Outcome = group.Where(x =>
                    x.CategoryId != Category.IncomeId &&
                    x.CategoryId != Category.InternalTransferId &&
                    x.CategoryId != Category.SkipId).Sum(x => x.Value),
            })
            .SortBy(x => x.Outcome).ThenBy(x => x.Income)
            .Group(x => new
            {
                x.Year,
                x.Month
            }, group => new GroupedTransactionsListDto.GroupedTransactions
            {
                Year = group.Key.Year,
                Month = group.Key.Month,
                Transactions = group.Select(x => new GroupedTransactionsListDto.TransactionsByCategory
                {
                    CategoryId = x.CategoryId,
                    CategoryName = categories.First(y => y.Id == x.CategoryId).Name,
                    Value = x.Income > 0 ? x.Income : x.Outcome,
                }).ToList(),
                Outcome = group.Sum(x => x.Outcome),
                Income = group.Sum(x => x.Income),
            })
            .SortByDescending(x => x.Year).ThenByDescending(x => x.Month)
            .ToListAsync(cancellationToken: cancellationToken);

        return new GroupedTransactionsListDto
        {
            Transactions = result
        };
    }
}