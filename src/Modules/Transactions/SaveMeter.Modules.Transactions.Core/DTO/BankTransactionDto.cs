using System;

namespace SaveMeter.Modules.Transactions.Core.DTO;

public record BankTransactionDto
{
    public Guid Id { get; init; }
    public DateTime TransactionDate { get; init; }
    public string Customer { get; init; }
    public string Description { get; init; }
    public decimal Value { get; init; }
    public Guid? CategoryId { get; init; }
    public string CategoryName { get; init; }
}