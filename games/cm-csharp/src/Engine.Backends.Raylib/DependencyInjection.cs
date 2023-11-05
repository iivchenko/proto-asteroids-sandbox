using Engine.Assets;
using Engine.Backends.Raylib.Assets;
using Engine.Backends.Raylib.Graphics;
using Engine.Backends.Raylib.Windows;
using Engine.Game.Graphics;
using Engine.Game.Windows;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection WithRayLibBackend(this IServiceCollection services)
    {
        services.AddSingleton<IWindowsSystem, RayLibWindowsSystem>();
        services.AddSingleton<RayLibAssetProvider>();
        services.AddSingleton<IAssetProvider>(x => x.GetRequiredService<RayLibAssetProvider>());
        services.AddSingleton<IGraphicsSystem, RayLibGraphicsSystem>();

        return services;
    }
}

