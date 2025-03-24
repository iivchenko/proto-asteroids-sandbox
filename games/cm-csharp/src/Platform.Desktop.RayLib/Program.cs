﻿using Engine.Backends.Raylib;
using Engine.Host;
using Game.EFS;

namespace Platform.Desktop.RayLib;


public static class AssetsG
{
    public static SpritesG Sprites { get; } = new SpritesG();
}

public class SpritesG
{
    public AsteroidsG Asteroids { get; } = new AsteroidsG();
}

public class AsteroidsG
{
    public BigG Big { get; } = new BigG();

    public MediumG Medium { get; } = new MediumG();

    public string[] Plain()
    {
        return
        [
            Big.AsteroidBig01,
            Medium.AsteroidMedium01
        ];
    }
}

public class BigG
{
    public string AsteroidBig01 { get; } = "Assets/Sprites/Asteroids/Big/AsteroidBig01.png";
}

public class MediumG
{
    public string AsteroidMedium01 { get; } = "Assets/Sprites/Asteroids/Big/AsteroidMedium01.png";
}

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

TODO:
- Introduce Source Generators to wrap assets pathes and make it Compile Time Access
- Implement JavaScript backend
  
 */
public static class Program
{
    public static void Main()
    {
        var a = AssetsG.Sprites.Asteroids.Big.AsteroidBig01;

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
                        Header = "Proto Asteroids [RayLib]",
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