using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SaveMeter.Modules.Transactions.Core.Entities;

namespace SaveMeter.Modules.Transactions.Core.Repositories
{
    internal interface IBankTransactionMlContext
    {
        byte[] TrainModel(IEnumerable<BankTransaction> bankTransactions);

        Task<Guid> Predicate(string customer, string description, Guid userId);
    }
}
    