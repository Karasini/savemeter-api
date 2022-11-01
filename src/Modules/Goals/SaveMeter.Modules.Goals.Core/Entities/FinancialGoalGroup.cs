using System;
using System.Collections.Generic;
using System.Linq;
using SaveMeter.Shared.Abstractions.Kernel.Types;

namespace SaveMeter.Modules.Goals.Core.Entities;

internal class FinancialGoalGroup : Entity
{
    public string Title { get; set; }
    public decimal TotalAmount { get; set; }
    public List<FinancialGoal> Goals { get; set; }

    private FinancialGoalGroup()
    {
        Goals = new List<FinancialGoal>();
    }

    public static FinancialGoalGroup Create(string title, List<FinancialGoal> goals)
    {
        return new FinancialGoalGroup
        {
            Title = title,
            Goals = goals,
            TotalAmount = goals.Sum(x => x.Amount)
        };
    }

    public void AddGoal(FinancialGoal goal)
    {
        Goals.Add(goal);
        UpdateTotalAmount();
    }

    private void UpdateTotalAmount()
    {
        TotalAmount = Goals.Sum(x => x.Amount);
    }

    public void UpdateGoal(FinancialGoal goal)
    {
        var goalIndex = Goals.FindIndex(x => x.Id == goal.Id);
        Goals[goalIndex] = goal;
        UpdateTotalAmount();
    }

    public void RemoveGoal(Guid goalId)
    {
        Goals.RemoveAll(x => x.Id == goalId);
        UpdateTotalAmount();
    }
}