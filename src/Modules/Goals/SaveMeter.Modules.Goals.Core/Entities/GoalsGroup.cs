using System;
using System.Collections.Generic;
using System.Linq;
using SaveMeter.Shared.Abstractions.Kernel.Types;

namespace SaveMeter.Modules.Goals.Core.Entities;

internal class GoalsGroup : Entity
{
    public string Title { get; private set; }
    public decimal TotalAmount { get; private set; }
    private List<Goal> _goals;
    public IEnumerable<Goal> Goals => _goals;
    
    private GoalsGroup()
    {
        _goals = new List<Goal>();
    }

    public static GoalsGroup Create(string title, List<Goal> goals)
    {
        return new GoalsGroup
        {
            Title = title,
            _goals = goals,
            TotalAmount = goals.Sum(x => x.Amount)
        };
    }

    public void AddGoal(Goal goal)
    {
        _goals.Add(goal);
        UpdateTotalAmount();
    }

    private void UpdateTotalAmount()
    {
        TotalAmount = _goals.Sum(x => x.Amount);
    }

    public void UpdateGoal(Goal goal)
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