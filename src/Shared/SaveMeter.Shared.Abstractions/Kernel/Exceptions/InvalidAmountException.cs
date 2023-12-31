﻿using SaveMeter.Shared.Abstractions.Exceptions;

namespace SaveMeter.Shared.Abstractions.Kernel.Exceptions;

public class InvalidAmountException : BaseException
{
    public decimal Amount { get; }

    public InvalidAmountException(decimal amount) : base($"Amount: '{amount}' is invalid.")
    {
        Amount = amount;
    }
}