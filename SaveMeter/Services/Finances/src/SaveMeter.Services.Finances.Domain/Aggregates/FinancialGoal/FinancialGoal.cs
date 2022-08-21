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

        public static FinancialGoal Create(string title, decimal amount)
        {
            return new FinancialGoal
            {
                Amount = amount,
                Title = title,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };
        }
    }
}
