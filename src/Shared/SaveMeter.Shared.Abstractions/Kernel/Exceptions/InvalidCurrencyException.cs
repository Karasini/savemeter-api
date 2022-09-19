using SaveMeter.Shared.Abstractions.Exceptions;

namespace SaveMeter.Shared.Abstractions.Kernel.Exceptions;

public class InvalidCurrencyException : SaveMeterException
{
    public string Currency { get; }

    public InvalidCurrencyException(string currency) : base($"Currency: '{currency}' is invalid.")
    {
        Currency = currency;
    }
}