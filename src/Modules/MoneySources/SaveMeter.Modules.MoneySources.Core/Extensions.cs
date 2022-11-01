using System.Runtime.CompilerServices;
using Microsoft.Extensions.DependencyInjection;
using SaveMeter.Modules.MoneySources.Core.DAL;
using SaveMeter.Modules.MoneySources.Core.DAL.Repositories;
using SaveMeter.Modules.MoneySources.Core.Repositories;
using SaveMeter.Shared.Infrastructure;

[assembly: InternalsVisibleTo("SaveMeter.Modules.MoneySources.Api")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]

namespace SaveMeter.Modules.MoneySources.Core;

internal static class Extensions
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        return services
            .AddScoped<MoneySourceReadRepository>()
            .AddScoped<IMoneySourceRepository, MoneySourceRepository>()
            .AddSchemaInitializer<MongoEntitiesInitializer>()
            .AddInitializer<MoneySourceInitializer>();
    }
}