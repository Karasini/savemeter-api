using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SaveMeter.Modules.Transactions.Core.DTO;
using SaveMeter.Shared.Abstractions.Commands;

namespace SaveMeter.Modules.Transactions.Core.Commands;
internal record CreateTransaction : ICommand<BankTransactionDto>
{
    public DateTime TransactionDateUtc { get; set; }
    public string Customer { get; set; }
    public string Description { get; set; }
    public decimal Value { get; set; }
    public string BankName { get; set; }
}
