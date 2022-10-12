using System.Collections.Generic;
using SaveMeter.Modules.Transactions.Core.Entities;

namespace SaveMeter.Modules.Transactions.Core.Repositories
{
    public interface IBankTransactionMlContext
    {
        void TrainModel(IEnumerable<BankTransaction> bankTransactions);

        string Predicate(string customer, string description);
    }
}
