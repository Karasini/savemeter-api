using SaveMeter.Shared.Abstractions.Exceptions;

namespace SaveMeter.Shared.Abstractions.Kernel.Exceptions;

public class InvalidEmailException : SaveMeterException
{
    public string Email { get; }

    public InvalidEmailException(string email) : base($"Email: '{email}' is invalid.")
    {
        Email = email;
    }
}