using Engine.Host;
using Engine.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Engine.Backends.Raylib;

public static class GameBuilderExtensions
{
    public static GameBuilder WithRayLibBackend(this GameBuilder builder)
    {
        builder.WithServices(services =>
        {
            services.AddSingleton<RayLibGraphicsSystem>();
            services.AddSingleton<IAssetService<Sprite>>(x => x.GetRequiredService<RayLibGraphicsSystem>());
            services.AddSingleton<IGraphicsService>(x => x.GetRequiredService<RayLibGraphicsSystem>());
            services.AddSingleton<IGame, RayLibGame>();
        });

        return builder;
    }
}

