using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaveMeter.Services.Finances.Application.DTO
{
    public record BankTransactionDto
    {
        public Guid Id { get; init; }
        public DateTime TransactionDate { get; init; }
        public string Customer { get; init; }
        public string Description { get; init; }
        public decimal Value { get; init; }
        public Guid? CategoryId { get; init; }
        public string? CategoryName { get; init; }
        public bool SkipAnalysis { get; init; }
    }
}
