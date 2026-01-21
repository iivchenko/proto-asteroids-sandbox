using Engine.EFS;
using Engine.EFS.Systems;
using Engine.Services;
using Game.EFS.Entities;
using Game.EFS.Systems;
using Microsoft.Extensions.DependencyInjection;

namespace Game.EFS;

public static class DependencyInjection
{
    public static IServiceCollection WithGameServices(this IServiceCollection services)
    {
        services
            .AddSingleton<IRandomService, RandomService>()
            .AddSingleton<IEntityBuilderFactory<PlayerBuilder>, PlayerBuilderFactory>()
            .AddSingleton<IEntityBuilderFactory<AsteroidBuilder>, AsteroidsBuilderFactory>()
            .AddTransient<ISystem, CollideSystem>()
            .AddTransient<ISystem, DrawSystem>()
            .AddTransient<ISystem, MoveSystem>()
            .AddTransient<ISystem, OutOfScreenSystem>()
            .AddTransient<ISystem, PlayerControlSystem>()
            .AddTransient<IWorld, World>();

        return services;
    }
}