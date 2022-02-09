using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Instapp.Common.Exception.Models;

namespace SaveMeter.Services.Finances.Application.Exceptions
{
    internal class BankTransactionNotFoundException : NotFoundException
    {
        public override string Code => "bank_transaction_not_found";
    }
}
