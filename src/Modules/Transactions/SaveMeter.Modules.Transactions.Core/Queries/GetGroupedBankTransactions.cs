using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SaveMeter.Modules.Transactions.Core.DTO;
using SaveMeter.Shared.Abstractions.Queries;

namespace SaveMeter.Modules.Transactions.Core.Queries;

internal record GetGroupedBankTransactions : IQuery<GroupedTransactionsListDto>
{
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public Guid UserId { get; set; }
}
