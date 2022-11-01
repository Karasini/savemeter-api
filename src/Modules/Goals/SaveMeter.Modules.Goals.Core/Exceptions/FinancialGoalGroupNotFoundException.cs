using SaveMeter.Shared.Abstractions.Exceptions;

namespace SaveMeter.Modules.Goals.Core.Exceptions
{
    internal class FinancialGoalGroupNotFoundException : NotFoundException
    {
        public override string Code => "financial_goal_group_not_found";
    }
}
