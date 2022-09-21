using System;
using System.Collections.Generic;

namespace SaveMeter.Shared.Abstractions.Auth;

public interface IAuthManager
{
    string CreateToken(Guid userId, string role = null, string audience = null,
        IDictionary<string, IEnumerable<string>> claims = null);
}