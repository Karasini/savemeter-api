using Convey;
using Instapp.Common.Cqrs;
using Instapp.Common.Infrastructure;
using Instapp.Common.MongoDb;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using SaveMeter.Services.Finances.Domain.Repositories;
using SaveMeter.Services.Finances.Infrastructure.MachineLearning;
using SaveMeter.Services.Finances.Infrastructure.Mongo;
using SaveMeter.Services.Finances.Infrastructure.Mongo.Repositories;

namespace SaveMeter.Services.Finances.Infrastructure
{
    public static class Extensions
    {
        public static IConveyBuilder AddInfrastructure(this IConveyBuilder builder)
        {
            MongoConfiguration.RegisterEntities();
            var machineLearningOptions = builder.GetOptions<MachineLearningOptions>("machineLearning");

            builder
                .AddInfrastructureBase()
                .AddMongoDb()
                .Services
                    .AddCqrs(typeof(Extensions).Assembly, typeof(Application.Extensions).Assembly)
                    .AddScoped<ICategoryRepository, CategoryRepository>()
                    .AddScoped<IBankTransactionRepository, BankTransactionRepository>()
                    .AddScoped<ICategoryReferenceRepository, CategoryReferenceRepository>()
                    .AddScoped<BankTransactionReadRepository>()
                    .AddScoped<CategoryReadRepository>()
                    .AddScoped<MoneySourceReadRepository>()
                    .AddScoped<IMoneySourceRepository, MoneySourceRepository>()
                    .AddScoped<IFinancialGoalGroupRepository, FinancialGoalGroupRepository>()
                    .AddScoped<FinancialGoalGroupReadRepository>()
                    .AddScoped<IBankTransactionMlContext, BankTransactionMlContext>()
                .AddScoped<MongoSeed>();

            return builder;
        }

        public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app)
        {
            app
                .UseInfrastructureBase();

            using var scope = app.ApplicationServices.CreateScope();
            var seed = scope.ServiceProvider.GetRequiredService<MongoSeed>();
            seed.Seed().Wait();

            return app;
        }
    }
}
