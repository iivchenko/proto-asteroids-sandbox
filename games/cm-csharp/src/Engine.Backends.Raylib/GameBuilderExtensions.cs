using Engine.Host;
using Engine.Services;
using Engine.Services.Keyboard;
using Microsoft.Extensions.DependencyInjection;

namespace Engine.Backends.Raylib;

public static class GameBuilderExtensions
{
    public static GameBuilder WithRayLibBackend(this GameBuilder builder)
    {
        builder.WithServices(services =>
        {
            services.AddSingleton<RayLibGame_GraphicsService>();
            services.AddSingleton<IAssetService<Sprite>>(x => x.GetRequiredService<RayLibGame_GraphicsService>());
            services.AddSingleton<IGraphicsService>(x => x.GetRequiredService<RayLibGame_GraphicsService>());
            services.AddSingleton<IViewService, RayLibGame_ViewService>();
            services.AddSingleton<IKeyboardService, RayLibGame_KeyboardService>();
            services.AddSingleton<IGame, RayLibGame>();
        });

        return builder;
    }
}

