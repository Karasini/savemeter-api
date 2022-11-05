using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using SaveMeter.Modules.Users.Core.DAL.Repositories;
using SaveMeter.Modules.Users.Core.Entities;
using SaveMeter.Modules.Users.Core.Repositories;
using SaveMeter.Shared.Infrastructure;
using System.Runtime.CompilerServices;
using SaveMeter.Modules.Users.Core.DAL;

[assembly: InternalsVisibleTo("SaveMeter.Modules.Users.Api")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]

namespace SaveMeter.Modules.Users.Core;

internal static class Extensions
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        var registrationOptions = services.GetOptions<RegistrationOptions>("users:registration");
        services.AddSingleton(registrationOptions);

        return services
            .AddScoped<IRoleRepository, RoleRepository>()
            .AddScoped<IUserRepository, UserRepository>()
            .AddScoped<RoleReadRepository>()
            .AddScoped<UserReadRepository>()
            .AddSchemaInitializer<MongoEntitiesInitializer>()
            .AddInitializer<RoleInitializer>()
            .AddInitializer<UsersInitializer>()
            .AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>();
    }
}