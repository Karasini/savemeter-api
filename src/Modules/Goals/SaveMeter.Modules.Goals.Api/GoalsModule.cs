using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using SaveMeter.Modules.Goals.Core;
using SaveMeter.Shared.Abstractions.Modules;

namespace SaveMeter.Modules.Goals.Api;

internal class GoalsModule : IModule
{
    public string Name => "Goals";

    public IEnumerable<string> Policies { get; } = new[]
    {
        Claims.GoalsCrud, Claims.GoalsRead
    };

    public void Register(IServiceCollection services)
    {
        services.AddCore();
    }

    public void Use(IApplicationBuilder app)
    {
    }
}
