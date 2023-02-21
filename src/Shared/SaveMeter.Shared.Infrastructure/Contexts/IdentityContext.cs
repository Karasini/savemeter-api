using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using SaveMeter.Shared.Abstractions.Contexts;

namespace SaveMeter.Shared.Infrastructure.Contexts;

public class IdentityContext : IIdentityContext
{
    public bool IsAuthenticated { get; }
    public Guid Id { get; }
    public string Role { get; }

    public Dictionary<string, IEnumerable<string>> Claims { get; }
    public DateTimeOffset ExpirationTime { get; }

    private IdentityContext()
    {
    }

    public IdentityContext(Guid? id)
    {
        Id = id ?? Guid.Empty;
        IsAuthenticated = id.HasValue;
    }

    public IdentityContext(ClaimsPrincipal principal)
    {
        if (principal?.Identity is null || string.IsNullOrWhiteSpace(principal.Identity.Name))
        {
            return;
        }
            
        IsAuthenticated = principal.Identity?.IsAuthenticated is true;
        Id = IsAuthenticated ? Guid.Parse(principal.Identity.Name) : Guid.Empty;
        ExpirationTime = DateTimeOffset.FromUnixTimeSeconds(Convert.ToInt64(principal.Claims.SingleOrDefault(x => x.Type == "exp")?.Value));
        Role = principal.Claims.SingleOrDefault(x => x.Type == ClaimTypes.Role)?.Value;
        Claims = principal.Claims.GroupBy(x => x.Type)
            .ToDictionary(x => x.Key, x => x.Select(c => c.Value.ToString()));
    }

    public bool IsUser() => Role is "user";
        
    public bool IsAdmin() => Role is "admin";
        
    public static IIdentityContext Empty => new IdentityContext();
}