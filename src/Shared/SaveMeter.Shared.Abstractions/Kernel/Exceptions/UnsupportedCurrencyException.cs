using SaveMeter.Shared.Abstractions.Exceptions;

namespace SaveMeter.Shared.Abstractions.Kernel.Exceptions;

public class UnsupportedCurrencyException : BaseException
{
    public string Currency { get; }

    public UnsupportedCurrencyException(string currency) : base($"Currency: '{currency}' is unsupported.")
    {
        Currency = currency;
    }
}