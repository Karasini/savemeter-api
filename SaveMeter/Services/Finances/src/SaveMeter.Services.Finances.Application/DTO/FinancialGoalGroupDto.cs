using SaveMeter.Services.Finances.Domain.Aggregates.FinancialGoal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaveMeter.Services.Finances.Application.DTO
{
    public class FinancialGoalGroupDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public decimal TotalAmount { get; set; }
        public List<FinancialGoalDto> Goals { get; set; }
    }

    public class FinancialGoalDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public decimal Amount { get; set; }
        public string Rank { get; set; }
    }
}
