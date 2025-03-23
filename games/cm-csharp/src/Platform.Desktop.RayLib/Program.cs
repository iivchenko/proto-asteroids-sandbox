using Engine.Backends.Raylib;
using Engine.Host;
using Game.EIS;
using Microsoft.Extensions.DependencyInjection;

namespace Platform.Desktop.RayLib;

// Simple
// Modular
// Extensible
// Modern
// Cross platform
// 2d
public static class Program
{
    public static void Main()
    {
        var game =
            GameBuilder
                .CreateBuilder()
                .WithRayLibBackend()
                .WithServices(services =>
                {
                    services
                        .WithGameServices();
                })
                .WithConfiguration(config =>
                {
                    config.Window = new()
                    {
                        Header = "Proto Asteroids",
                        Width = 1024,
                        Height = 800
                    };
                })
                .WithBootstrapScene<GameBootstrapScene>()
                .Build();

        using (game)
        {
            game.Run();
        }
    }
}