using System.Runtime.CompilerServices;
using Microsoft.Extensions.DependencyInjection;
using SaveMeter.Modules.Transactions.Core.DAL;
using SaveMeter.Modules.Transactions.Core.DAL.Repositories;
using SaveMeter.Modules.Transactions.Core.MachineLearning;
using SaveMeter.Modules.Transactions.Core.Repositories;
using SaveMeter.Shared.Infrastructure;

[assembly: InternalsVisibleTo("SaveMeter.Modules.Transactions.Api")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]

namespace SaveMeter.Modules.Transactions.Core;

internal static class Extensions
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        return services           
            .AddScoped<ICategoryRepository, CategoryRepository>()
            .AddScoped<IBankTransactionRepository, BankTransactionRepository>()
            .AddScoped<IBankTransactionMlContext, BankTransactionMlContext>()
            .AddScoped<CategoryReadRepository>()
            .AddScoped<BankTransactionReadRepository>()
            .AddSchemaInitializer<MongoEntitiesInitializer>()
            .AddInitializer<CategoryInitializer>()
            .AddInitializer<TransactionsInitializer>();
    }
}