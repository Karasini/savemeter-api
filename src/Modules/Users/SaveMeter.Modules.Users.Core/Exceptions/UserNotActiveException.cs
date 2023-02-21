using SaveMeter.Shared.Abstractions.Exceptions;
using System;

namespace SaveMeter.Modules.Users.Core.Exceptions;

internal class UserNotActiveException : BaseException
{
    public Guid UserId { get; }

    public UserNotActiveException(Guid userId) : base($"User with ID: '{userId}' is not active.")
    {
        UserId = userId;
    }
}