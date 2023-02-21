using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using SaveMeter.Modules.Transactions.Api.Csv;
using SaveMeter.Modules.Transactions.Core;
using SaveMeter.Shared.Abstractions.Modules;
using SaveMeter.Shared.Infrastructure;

namespace SaveMeter.Modules.Transactions.Api;

internal class TransactionsModule : IModule
{
    public string Name => "Transactions";

    public IEnumerable<string> Policies { get; } = new[]
    {
        TransactionsPolicies.TransactionsCrud, TransactionsPolicies.TransactionsRead,
        TransactionsPolicies.CategoriesCrud, TransactionsPolicies.CategoriesRead
    };

    public void Register(IServiceCollection services)
    {
        var registrationOptions = services.GetOptions<CsvOptions>("transactions:csv");
        services.AddSingleton(registrationOptions);
        
        services.AddCore()
            .AddScoped<ITransactionImporter, TransactionImporter>();
    }

    public void Use(IApplicationBuilder app)
    {
    }
}
