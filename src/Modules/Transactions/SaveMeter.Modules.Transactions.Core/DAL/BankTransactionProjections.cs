using MongoDB.Driver;
using SaveMeter.Modules.Transactions.Core.DTO;
using SaveMeter.Modules.Transactions.Core.Entities;

namespace SaveMeter.Modules.Transactions.Core.DAL
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
