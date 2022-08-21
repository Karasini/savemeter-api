using Instapp.Common.Cqrs.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SaveMeter.Services.Finances.Application.DTO;

namespace SaveMeter.Services.Finances.Application.Queries
{
    public class GetFinancialGoalGroupsQuery : QueryBase<List<FinancialGoalGroupDto>>
    {
    }
}
