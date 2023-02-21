using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using SaveMeter.Modules.MoneySources.Core;
using SaveMeter.Shared.Abstractions.Modules;

namespace SaveMeter.Modules.MoneySources.Api;

internal class MoneySourcesModule : IModule
{
    public string Name => "MoneySources";

    public IEnumerable<string> Policies { get; } = new[]
    {
        Claims.MoneySourcesCrud, Claims.MoneySourcesRead
    };

    public void Register(IServiceCollection services)
    {
        services.AddCore();
    }

    public void Use(IApplicationBuilder app)
    {
    }
}
