using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Instapp.Common.MongoDb.Models;

namespace SaveMeter.Services.Finances.Domain.Aggregates.FinancialGoal
{
    public class FinancialGoal : Entity
    {
        public string Title { get; set; }
        public decimal Amount { get; set; }

        public static FinancialGoal Create(string title, decimal amount, Guid? id = null)
        {
            return new FinancialGoal
            {
                Id = id ?? Guid.NewGuid(),
                Amount = amount,
                Title = title,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };
        }
    }
}
