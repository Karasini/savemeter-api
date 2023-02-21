using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using SaveMeter.Modules.Users.Core;
using SaveMeter.Shared.Abstractions.Modules;

namespace SaveMeter.Modules.Users.Api;

internal class UsersModule : IModule
{
    public string Name => "Users";

    public IEnumerable<string> Policies { get; } = new[]
    {
        "users"
    };

    public void Register(IServiceCollection services)
    {
        services.AddCore();
    }

    public void Use(IApplicationBuilder app)
    {
    }
}
