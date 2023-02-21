using SaveMeter.Shared.Abstractions.Exceptions;

namespace SaveMeter.Shared.Abstractions.Kernel.Exceptions;

public class InvalidEmailException : BaseException
{
    public string Email { get; }

    public InvalidEmailException(string email) : base($"Email: '{email}' is invalid.")
    {
        Email = email;
    }
}