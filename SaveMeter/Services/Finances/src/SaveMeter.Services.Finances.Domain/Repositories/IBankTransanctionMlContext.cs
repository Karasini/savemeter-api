using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.IdGenerators;
using SaveMeter.Services.Finances.Domain.Aggregates.Transaction;

namespace SaveMeter.Services.Finances.Domain.Repositories
{
    public interface IBankTransactionMlContext
    {
        void TrainModel(IEnumerable<BankTransaction> bankTransactions);

        string Predicate(string customer, string description);
    }
}
