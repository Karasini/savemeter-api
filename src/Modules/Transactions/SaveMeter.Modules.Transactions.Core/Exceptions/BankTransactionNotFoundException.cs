using SaveMeter.Shared.Abstractions.Exceptions;

namespace SaveMeter.Modules.Transactions.Core.Exceptions
{
    internal class BankTransactionNotFoundException : BaseException
    {
        public BankTransactionNotFoundException() : base("bank_transaction_not_found")
        {
        }
    }
}
