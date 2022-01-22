using Convey;
using Instapp.Common.Cqrs;
using Instapp.Common.Infrastructure;
using Instapp.Common.MongoDb;
using Instapp.Services.Finances.Domain.Repositories;
using Instapp.Services.Finances.Infrastructure.MachineLearning;
using Instapp.Services.Finances.Infrastructure.Mongo;
using Instapp.Services.Finances.Infrastructure.Mongo.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Instapp.Services.Finances.Infrastructure
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
                    .AddScoped<ITransactionRepository, TransactionRepository>()
                    .AddScoped<ICategoryReferenceRepository, CategoryReferenceRepository>()
                    .AddSingleton(machineLearningOptions)
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
