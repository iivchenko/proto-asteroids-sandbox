using Engine.Backends.Sfml;
using Engine.Host;
using Game.EFS;

namespace Platform.Desktop.Sfml;

public static class Program
{
    public static void Main()
    {
        var game =
            GameBuilder
                .CreateBuilder()
                .WithSfmlBackend()
                .WithServices(services =>
                {
                    services
                        .WithGameServices();
                })
                .WithConfiguration(config =>
                {
                    config.Window = new()
                    {
                        Header = "Proto Asteroids [SFML]",
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