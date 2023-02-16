using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LinqKit;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
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

        var categoriesTask = _categoryRepository.Collection.Aggregate()
            .ToListAsync(cancellationToken: cancellationToken);

        var aggregationTask = _transactionRepository.Collection
            .Aggregate()
            .Match(filter.Expand())
            .Group(new BsonDocument
            {
                {
                    "_id",
                    new BsonDocument
                    {
                        {
                            "year",
                            new BsonDocument("$year", "$TransactionDate")
                        },
                        {
                            "month",
                            new BsonDocument("$month", "$TransactionDate")
                        }
                    }
                },
                {
                    "Transactions",
                    new BsonDocument("$push", "$$ROOT")
                }
            })
            .Project(x => new
            {
                Transactions = x["Transactions"],
                Year = x["_id"]["year"],
                Month = x["_id"]["month"]
            })
            .SortByDescending(x => x.Year).ThenByDescending(x => x.Month)
            .ToListAsync(cancellationToken: cancellationToken);

        await Task.WhenAll(aggregationTask, categoriesTask);
        var groupedTransactions = await aggregationTask;
        var categories = await categoriesTask;

        var result = new GroupedTransactionsListDto();

        foreach (var obj in groupedTransactions)
        {
            var transactions =
                obj.Transactions.AsBsonArray.Select(x => BsonSerializer.Deserialize<BankTransaction>(x.AsBsonDocument))
                    .ToList();

            var monthGroup = new GroupedTransactionsListDto.GroupedTransactions()
            {
                MonthOfYear = new DateTime(obj.Year.ToInt32(), obj.Month.ToInt32(), 1),
                Income = transactions.Where(x => x.CategoryId == Category.IncomeId).Sum(x => x.Value),
                Outcome = transactions.Where(x => Category.IsCategoryIdOutcome(x.CategoryId)).Sum(x => x.Value),
                TransactionsByCategories = categories.Select(x => new GroupedTransactionsListDto.TransactionsByCategory
                    {
                        CategoryId = x.Id,
                        Value = transactions.Where(bankTransaction => bankTransaction.CategoryId == x.Id)
                            .Sum(bankTransaction => bankTransaction.Value),
                        CategoryName = x.Name
                    })
                    .OrderBy(x => x.CategoryName)
                    .ToList()
            };
            result.Transactions.Add(monthGroup);
        }

        return result;
    }
}