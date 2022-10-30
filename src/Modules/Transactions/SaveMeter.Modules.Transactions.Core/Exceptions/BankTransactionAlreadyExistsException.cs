using System;
using SaveMeter.Shared.Abstractions.Exceptions;

namespace SaveMeter.Modules.Transactions.Core.Exceptions
{
    internal class BankTransactionAlreadyExistsException : BaseException
    {
        public BankTransactionAlreadyExistsException(DateTime date, decimal value) : base(
            $"Bank transaction with {date} and {value} already exists")
        {
        }
    }
}
