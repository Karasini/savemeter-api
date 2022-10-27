using Instapp.Common.Cqrs.Commands;
using SaveMeter.Services.Finances.Application.DTO;

namespace SaveMeter.Services.Finances.Application.Commands.CreateFinancialGoalGroup
{
    public class CreateFinancialGoalGroupCommand : CommandBase<FinancialGoalGroupDto>
    {
        public class FinancialGoalCommandDto
        {
            public string Title { get; set; }
            public decimal Amount { get; set; }
            public string Rank { get; set; }
        }

        public string Title { get; set; }
        public List<FinancialGoalCommandDto> Goals { get; set; }
    }
}
