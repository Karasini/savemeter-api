using Instapp.Common.MongoDb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Conventions;

namespace SaveMeter.Services.Finances.Domain.Aggregates.FinancialGoal
{
    public class FinancialGoalGroup : Entity, IAggregateRoot
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
            TotalAmount = Goals.Sum(x => x.Amount);
        }
    }
}
