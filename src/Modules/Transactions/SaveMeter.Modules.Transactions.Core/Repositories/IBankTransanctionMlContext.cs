using System.Collections.Generic;
using SaveMeter.Modules.Transactions.Core.Entities;

namespace SaveMeter.Modules.Transactions.Core.Repositories
{
    internal interface IBankTransactionMlContext
    {
        void TrainModel(IEnumerable<BankTransaction> bankTransactions);

        string Predicate(string customer, string description);
    }
}
