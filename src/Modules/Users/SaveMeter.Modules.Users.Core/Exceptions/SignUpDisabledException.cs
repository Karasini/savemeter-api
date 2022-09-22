using SaveMeter.Shared.Abstractions.Exceptions;

namespace SaveMeter.Modules.Users.Core.Exceptions;

internal class SignUpDisabledException : BaseException
{
    public SignUpDisabledException() : base("Sign up is disabled.")
    {
    }
}