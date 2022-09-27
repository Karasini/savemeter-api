using Microsoft.Extensions.DependencyInjection;
using SaveMeter.Shared.Abstractions.Commands;
using SaveMeter.Shared.Infrastructure.Mongo.Context;
using SaveMeter.Shared.Infrastructure.Mongo.Decorators;
using SaveMeter.Shared.Infrastructure.Mongo.UoW;

namespace SaveMeter.Shared.Infrastructure.Mongo
{
    public static class Extensions
    {
        public static IServiceCollection AddMongoDb(this IServiceCollection services)
        {
            var options = services.GetOptions<MongoOptions>("mongoDb");

            services
                .AddSingleton(options)
                .AddScoped<IMongoContext, MongoContext>()
                .AddScoped<IUnitOfWork, UnitOfWork>();

            services.TryDecorate(typeof(ICommandHandler<>), typeof(UnitOfWorkDecorator<>));
            services.TryDecorate(typeof(ICommandHandler<,>), typeof(UnitOfWorkDecoratorWithResult<,>));

            return services;
        }
    }
}
