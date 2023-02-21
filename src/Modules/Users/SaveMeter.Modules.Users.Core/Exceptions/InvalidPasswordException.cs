using SaveMeter.Shared.Abstractions.Exceptions;

namespace SaveMeter.Modules.Users.Core.Exceptions;

internal class InvalidPasswordException : BaseException
{
    public string Reason { get; }

    public InvalidPasswordException(string reason) : base($"Invalid password: {reason}.")
    {
        Reason = reason;
    }
}