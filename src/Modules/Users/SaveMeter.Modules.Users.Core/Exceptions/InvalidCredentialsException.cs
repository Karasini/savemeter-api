using SaveMeter.Shared.Abstractions.Exceptions;

namespace SaveMeter.Modules.Users.Core.Exceptions;

internal class InvalidCredentialsException : BaseException
{
    public InvalidCredentialsException() : base("Invalid credentials.")
    {
    }
}