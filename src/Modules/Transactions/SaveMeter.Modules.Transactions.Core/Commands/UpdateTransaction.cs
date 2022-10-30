using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SaveMeter.Modules.Transactions.Core.DTO;
using SaveMeter.Shared.Abstractions.Commands;

namespace SaveMeter.Modules.Transactions.Core.Commands;
internal record UpdateTransaction : ICommand<BankTransactionDto>
{
    public Guid Id { get; set; }
    public string Customer { get; init; }
    public string Description { get; init; }
    public Guid? CategoryId { get; init; }
    public Guid UserId { get; set; }
}
