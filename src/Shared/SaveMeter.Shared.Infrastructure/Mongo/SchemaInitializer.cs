using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SaveMeter.Shared.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SaveMeter.Shared.Infrastructure.Mongo;
internal class SchemaInitializer : IHostedService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<SchemaInitializer> _logger;

    public SchemaInitializer(IServiceProvider serviceProvider, ILogger<SchemaInitializer> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var initializers = scope.ServiceProvider.GetServices<ISchemaInitializer>();

        foreach (var initializer in initializers)
        {
            try
            {
                _logger.LogInformation($"Running the schema initializer: {initializer.GetType().Name}...");
                initializer.Initialize();
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);
            }
        }

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}
