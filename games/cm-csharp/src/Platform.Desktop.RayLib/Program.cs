using Engine.Backends.Raylib;
using Engine.Host;
using Game.EFS;

namespace Platform.Desktop.RayLib;

// Simple
// Modular
// Extensible
// Modern
// Cross platform
// 2d
// Name Suggestions:
// - Collaps
// - CLab
// - Science Lab
// - Duck Pack
// - Science Pack
// - Game Pack

/*
 * Proto Asteroids Monogame CS Classes&Interfaces

Use classes to describe GameObjects (no components or nodes etc.)
Use Interfaces to specialize functionality of the GameObjects (IBody  for collision etc., IDrawable for Sprite drawing)
Use Systems to consume this Interfaces and proivce outside the GameObject functionality
What about IEntity to have OnMsg method so any system can send a message or event to an entity for instance Collision System will send msgs/events about collision with other objects
Get rid of the DI and Container, game settings, score board etc.
Introduce code generation to make acces to the assets compile time

TODO:
- Migrate to latest MonoGame
- Break depenency with Comora
- Break game an engine depenency from MonoGame libs
- Implement second back end - Raylib
- May be even third - JavaScript
  
 */
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