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
using SaveMeter.Services.Finances.Domain.Aggregates.Transaction;
using SaveMeter.Services.Finances.Infrastructure.Mongo;
using SaveMeter.Services.Finances.Infrastructure.Mongo.Repositories;
using PredicateBuilder = Instapp.Services.Identity.Infrastructure.PredicateBuilder;

namespace SaveMeter.Services.Finances.Infrastructure.QueryHandlers
{
    internal class GetBankTransactionsByFilterQueryHandler : IQueryHandler<GetBankTransactionsByFilterQuery, PaginatedDto<BankTransactionDto>>
    {
        private readonly BankTransactionReadRepository _repository;

        public GetBankTransactionsByFilterQueryHandler(BankTransactionReadRepository bankTransactionReadRepository)
        {
            _repository = bankTransactionReadRepository;
        }

        public async Task<PaginatedDto<BankTransactionDto>> Handle(GetBankTransactionsByFilterQuery request, CancellationToken cancellationToken)
        {
            var pageNumber = request.PageNumber ?? 1;
            var pageSize = request.PageSize ?? 100;
            var filter = PredicateBuilder.True<BankTransaction>().
                AndIfAcceptable(request.StartDate.HasValue, x => x.TransactionDate > request.StartDate!.Value)
                .AndIfAcceptable(request.EndDate.HasValue, x => x.TransactionDate < request.EndDate!.Value);

            var count = await _repository.Find(filter.Expand()).CountDocumentsAsync();

            var bankTransactions = await _repository.Find(filter.Expand())
                .ProjectToBankTransactionDto()
                .Skip((pageNumber - 1) * pageSize)
                .Limit(pageSize)
                .ToListAsync();

            return new PaginatedDto<BankTransactionDto>
            {
                CurrentPage = pageNumber,
                Items = bankTransactions,
                PageSize = pageSize,
                TotalCount = count,
                TotalPages = (int)Math.Ceiling(count / (double)pageSize)
            };
        }
    }
}
