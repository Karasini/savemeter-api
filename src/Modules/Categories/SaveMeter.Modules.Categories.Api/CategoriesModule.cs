using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using SaveMeter.Modules.Categories.Core;
using SaveMeter.Shared.Abstractions.Modules;

namespace SaveMeter.Modules.Categories.Api;

internal class CategoriesModule : IModule
{
    public string Name => "Categories";

    public IEnumerable<string> Policies { get; } = new[]
    {
        CategoryPolicies.CategoriesCrud, CategoryPolicies.CategoriesRead
    };

    public void Register(IServiceCollection services)
    {
        services.AddCore();
    }

    public void Use(IApplicationBuilder app)
    {
    }
}
