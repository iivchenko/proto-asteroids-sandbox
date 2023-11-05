using Engine.Game;
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
                    // TODO: Add window config
                    config.WindowHeader = "Proto Asteroids";
                    config.WindowWidth = 1024;
                    config.WindowHeight = 800;
                })
                .WithBootstrapScene<TempScene>()
                .Build();

        using (game)
        {
            game.Run();
        }
    }
}