using System;
using System.Collections.Generic;
using System.Linq;
using SaveMeter.Shared.Abstractions.Kernel.Types;

namespace SaveMeter.Modules.Goals.Core.Entities;

internal class FinancialGoalGroup : Entity
{
    public string Title { get; private set; }
    public decimal TotalAmount { get; private set; }
    private List<FinancialGoal> _goals;
    public IEnumerable<FinancialGoal> Goals => _goals;
    
    private FinancialGoalGroup()
    {
        _goals = new List<FinancialGoal>();
    }

    public static FinancialGoalGroup Create(string title, List<FinancialGoal> goals)
    {
        return new FinancialGoalGroup
        {
            Title = title,
            _goals = goals,
            TotalAmount = goals.Sum(x => x.Amount)
        };
    }

    public void AddGoal(FinancialGoal goal)
    {
        _goals.Add(goal);
        UpdateTotalAmount();
    }

    private void UpdateTotalAmount()
    {
        TotalAmount = _goals.Sum(x => x.Amount);
    }

    public void UpdateGoal(FinancialGoal goal)
    {
        var goalIndex = _goals.FindIndex(x => x.Id == goal.Id);
        _goals[goalIndex] = goal;
        UpdateTotalAmount();
    }

    public void RemoveGoal(Guid goalId)
    {
        _goals.RemoveAll(x => x.Id == goalId);
        UpdateTotalAmount();
    }
}