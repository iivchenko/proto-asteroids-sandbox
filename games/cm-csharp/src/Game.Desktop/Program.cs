using Engine.Host;
using Microsoft.Extensions.DependencyInjection;

namespace Game.Desktop;

public static class Program
{
    public static void Main()
    {
        var game =
            GameBuilder
                .CreateBuilder()
                 //.WithRayLibBackend() // TODO: Thinks on moving backend application to the builder level
                .WithServices(services =>
                {
                    services
                        .WithRayLibBackend() // TODO: Thinks on moving backend application to the builder level
                        .WithHostServices()
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