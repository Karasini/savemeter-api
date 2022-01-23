using MongoDB.Driver;
using SaveMeter.Services.Finances.Application.DTO;
using SaveMeter.Services.Finances.Domain.Aggregates.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaveMeter.Services.Finances.Infrastructure.Mongo
{
    static class BankTransactionProjections
    {
        public static ProjectionDefinition<BankTransaction, BankTransactionDto> Projection =>
    Builders<BankTransaction>.Projection.Expression(bankTransaction => new BankTransactionDto
    {
        Id = bankTransaction.Id,
        CategoryId = bankTransaction.CategoryId,
        Customer = bankTransaction.Customer,
        Description = bankTransaction.Description,
        SkipAnalysis = bankTransaction.SkipAnalysis,
        TransactionDate = bankTransaction.TransactionDate,
        Value = bankTransaction.Value,
        CategoryName = bankTransaction.Categories.Count > 0 ? bankTransaction.Categories.First().Name : null
    });

        public static IFindFluent<BankTransaction, BankTransactionDto> ProjectToBankTransactionDto(this IFindFluent<BankTransaction, BankTransaction> fluent)
        {
            return fluent.Project(Projection);
        }

        public static IAggregateFluent<BankTransactionDto> ProjectToBankTransactionDto(this IAggregateFluent<BankTransaction> fluent)
        {
            return fluent.Project(Projection);
        }
    }
}
