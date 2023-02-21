using SaveMeter.Shared.Abstractions.Exceptions;

namespace SaveMeter.Modules.Users.Core.Exceptions;

internal class EmailInUseException : BaseException
{
    public EmailInUseException() : base("Email is already in use.")
    {
    }
}