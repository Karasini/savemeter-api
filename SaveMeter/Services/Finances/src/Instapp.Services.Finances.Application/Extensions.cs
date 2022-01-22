using Convey;
using Instapp.Common.Application;
using Microsoft.AspNetCore.Builder;

namespace Instapp.Services.Finances.Application
{
    public static class Extensions
    {
        public static IConveyBuilder AddApplication(this IConveyBuilder builder)
        {
            builder
                .AddApplicationBase();

            return builder;
        }

        public static IApplicationBuilder UseApplication(this IApplicationBuilder app)
        {
            app
                .UseApplicationBase();

            return app;
        }
    }
}
