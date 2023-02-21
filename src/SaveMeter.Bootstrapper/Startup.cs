
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SaveMeter.Shared.Abstractions.Modules;
using SaveMeter.Shared.Infrastructure;
using SaveMeter.Shared.Infrastructure.Contracts;
using SaveMeter.Shared.Infrastructure.Modules;

namespace SaveMeter.Bootstrapper;

internal class Startup
{
    private readonly IList<Assembly> _assemblies;
    private readonly IList<IModule> _modules;

    public Startup(IConfiguration configuration)
    {
        _assemblies = ModuleLoader.LoadAssemblies(configuration, "SaveMeter.Modules.");
        _modules = ModuleLoader.LoadModules(_assemblies);
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddModularInfrastructure(_assemblies, _modules);
        foreach (var module in _modules)
        {
            module.Register(services);
        }
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
    {
        logger.LogInformation($"Modules: {string.Join(", ", _modules.Select(x => x.Name))}");
        app.UseModularInfrastructure();
        foreach (var module in _modules)
        {
            module.Use(app);
        }

        app.ValidateContracts(_assemblies);
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapGet("/", context => context.Response.WriteAsync("SaveMeter API"));
            endpoints.MapModuleInfo();
        });

        _assemblies.Clear();
        _modules.Clear();
    }
}