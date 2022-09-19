using System;

namespace SaveMeter.Shared.Abstractions.Exceptions;

public abstract class SaveMeterException : Exception
{
    protected SaveMeterException(string message) : base(message)
    {
    }
}