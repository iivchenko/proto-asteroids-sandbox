using Engine.Assets;
using Engine.Backends.Raylib.Graphics;
using Engine.Backends.Raylib.Windows;
using Engine.Graphics;
using Engine.Host.Graphics;
using Engine.Host.Windows;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection WithRayLibBackend(this IServiceCollection services)
    {
        services.AddSingleton<IWindowsSystem, RayLibWindowsSystem>();
        services.AddSingleton<RayLibGraphicsSystem>();
        services.AddSingleton<IAssetLoader<Sprite>>(x => x.GetRequiredService<RayLibGraphicsSystem>());
        services.AddSingleton<IGraphicsSystem>(x => x.GetRequiredService<RayLibGraphicsSystem>());

        return services;
    }
}

