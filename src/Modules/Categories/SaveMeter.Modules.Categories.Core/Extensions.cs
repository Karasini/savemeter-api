using System.Runtime.CompilerServices;
using Microsoft.Extensions.DependencyInjection;
using SaveMeter.Modules.Categories.Core.DAL;
using SaveMeter.Modules.Categories.Core.DAL.Repositories;
using SaveMeter.Modules.Categories.Core.Repositories;
using SaveMeter.Shared.Infrastructure;

[assembly: InternalsVisibleTo("SaveMeter.Modules.Categories.Api")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]

namespace SaveMeter.Modules.Categories.Core;

internal static class Extensions
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        return services
            .AddScoped<ICategoryRepository, CategoryRepository>()
            .AddScoped<CategoryReadRepository>()
            .AddSchemaInitializer<MongoEntitiesInitializer>()
            .AddInitializer<CategoryInitializer>();
    }
}