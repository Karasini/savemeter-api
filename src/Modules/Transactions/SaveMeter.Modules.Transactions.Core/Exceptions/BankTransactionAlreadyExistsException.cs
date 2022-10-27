using SaveMeter.Shared.Abstractions.Exceptions;

namespace SaveMeter.Modules.Transactions.Core.Exceptions
{
    internal class BankTransactionAlreadyExistsException : BaseException
    {
        public BankTransactionAlreadyExistsException() : base("Bank transaction already exists")
        {
        }
    }
}
