using SaveMeter.Shared.Abstractions.Exceptions;

namespace SaveMeter.Modules.Users.Core.Exceptions;

internal class RoleNotFoundException : BaseException
{
    public RoleNotFoundException(string role) : base($"Role: '{role}' was not found.")
    {
    }
}