using System.Collections.Generic;
using SaveMeter.Modules.Goals.Core.DTO;
using SaveMeter.Shared.Abstractions.Commands;

namespace SaveMeter.Modules.Goals.Core.Commands;

internal class CreateGoalGroup : ICommand<GoalsGroupDto>
{
    internal class FinancialGoalCommandDto
    {
        public string Title { get; set; }
        public decimal Amount { get; set; }
        public string Rank { get; set; }
    }

    public string Title { get; set; }
    public List<FinancialGoalCommandDto> Goals { get; set; }
}