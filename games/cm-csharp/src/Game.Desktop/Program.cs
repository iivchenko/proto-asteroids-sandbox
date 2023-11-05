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
                .WithServices(services =>
                {
                    services
                        .WithRayLibBackend()
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
                .WithBootstrapScene<TempScene>()
                .Build();

        using (game)
        {
            game.Run();
        }
    }
}