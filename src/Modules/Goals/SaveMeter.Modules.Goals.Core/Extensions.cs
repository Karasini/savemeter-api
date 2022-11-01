using System.Runtime.CompilerServices;
using Microsoft.Extensions.DependencyInjection;
using SaveMeter.Modules.Goals.Core.DAL;
using SaveMeter.Modules.Goals.Core.DAL.Repositories;
using SaveMeter.Shared.Infrastructure;

[assembly: InternalsVisibleTo("SaveMeter.Modules.Goals.Api")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]

namespace SaveMeter.Modules.Goals.Core;

internal static class Extensions
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        return services
            .AddScoped<FinancialGoalGroupReadRepository>()
            .AddScoped<IFinancialGoalGroupRepository, FinancialGoalGroupRepository>()
            .AddSchemaInitializer<GoalsSchemaInitializer>();
    }
}