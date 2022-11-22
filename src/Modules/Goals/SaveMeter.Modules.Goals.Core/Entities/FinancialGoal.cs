using System;
using SaveMeter.Shared.Abstractions.Kernel.Types;

namespace SaveMeter.Modules.Goals.Core.Entities;

internal class FinancialGoal : Entity
{
    public string Title { get; private set; }
    public decimal Amount { get; private set; }
    public string Rank { get; private set; }

    public static FinancialGoal Create(string title, decimal amount, string rank, Guid? id = null)
    {
        return new FinancialGoal
        {
            Id = id ?? Guid.NewGuid(),
            Amount = amount,
            Title = title,
            Rank = rank,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
        };
    }
}