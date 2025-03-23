using Engine.EFS;
using Game.EFS.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace Game.EFS;

public static class DependencyInjection
{
    public static IServiceCollection WithGameServices(this IServiceCollection services)
    {
        services
            .AddSingleton<IEntityBuilderFactory<AsteroidBuilder>, AsteroidsBuilderFactory>();

        return services;
    }
}

