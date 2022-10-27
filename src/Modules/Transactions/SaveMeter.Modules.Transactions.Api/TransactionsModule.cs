using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using SaveMeter.Modules.Transactions.Core;
using SaveMeter.Shared.Abstractions.Modules;

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
        services.AddCore();
    }

    public void Use(IApplicationBuilder app)
    {
    }
}
