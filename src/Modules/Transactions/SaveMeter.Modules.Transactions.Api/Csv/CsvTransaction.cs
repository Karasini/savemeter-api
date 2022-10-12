using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaveMeter.Modules.Transactions.Api.Csv;
internal abstract record CsvTransaction
{
    public DateTime TransactionDateUtc { get; set; }
    public string RelatedAccountNumber { get; set; }
    public string Customer { get; set; }
    public string Description { get; set; }
    public decimal Value { get; set; }
    public decimal AccountBalance { get; set; }
    public abstract string BankName { get; }
}
