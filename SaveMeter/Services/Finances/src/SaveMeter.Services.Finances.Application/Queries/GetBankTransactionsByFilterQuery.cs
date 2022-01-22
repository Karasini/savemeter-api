using SaveMeter.Services.Finances.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaveMeter.Services.Finances.Application.Queries
{
    public class GetBankTransactionsByFilterQuery : PaginatedQuery<PaginatedDto<BankTransactionDto>>
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
