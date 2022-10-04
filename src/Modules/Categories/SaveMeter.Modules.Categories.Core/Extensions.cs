using System.Runtime.CompilerServices;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo("SaveMeter.Modules.Categories.Api")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]

namespace SaveMeter.Modules.Categories.Core;

internal static class Extensions
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        return services;
    }
}