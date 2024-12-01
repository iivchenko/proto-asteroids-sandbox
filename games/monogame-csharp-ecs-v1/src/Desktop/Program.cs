using Engine;
using Engine.Core;
using Engine.Ecs.Components;
using Engine.Ecs.Core;
using Engine.Ecs.Systems;
using Engine.MonoGame;
using Engine.Services.Content;
using Engine.Services.Draw;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace Desktop
{

    public sealed class Entity : IEntity
    {
        public Entity(Guid id, IEnumerable<IComponent> components)
        {
            Id = id;
            Components = components;
        }

        public Guid Id { get; }

        public IEnumerable<IComponent> Components { get; }
    }

    public sealed class EntryPoint : IEntryPoint
    {
        private readonly IContentService _contentService;

        private IEnumerable<IEntity> _entities;
        private readonly ISystem _drawSystem;

        public EntryPoint(
            IContentService contentService, 
            ISystem drawSystem)
        {
            _contentService = contentService;

            var sprite = _contentService.Load<Sprite>("Sprites/PlayerShips/PlayerShip01");
            var entity = new Entity
                (
                    Guid.NewGuid(),
                    [
                        new SpriteComponent(sprite, new Color(255, 255, 255, 255)),
                        new TransformComponent(new Vector(100, 100))
                    ]
                );

            _entities = new List<IEntity>
            {
                entity
            };

            _drawSystem = drawSystem;
        }

        public void Process(float detla)
        {
            _drawSystem.Run(_entities);
        }
    }

    public static class Program
    {
        [STAThread]
        public static void Main()
        {
            GameBuilder
                .CreateBuilder()
                .WithServices(services =>
                {
                    services
                        .AddSingleton<ISystem, DrawSystem>()
                        .AddSingleton<IEntryPoint, EntryPoint>()
                        .WithMonoGameBackend();

                })
                .Build()
                .Run();
        }

//        private const string ConfigFile = "game-settings.json";

//        [STAThread]
//        public static void Main()
//        {
//#if MacOS
//            var root = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "Resources");
//#else
//            var root = string.Empty;
//#endif
//            var configFilePath = Path.Combine(root, ConfigFile);
//            var contentRoot = root;
//            GameBuilder
//                .CreateBuilder()
//                .WithServices(container =>
//                    {
//                        var configuration =
//                            new ConfigurationBuilder()
//                                .SetBasePath(Directory.GetCurrentDirectory())
//                                .AddJsonFile(configFilePath, optional: true, reloadOnChange: true)
//                                .Build();

//                        container
//                            .AddOptions()
//                            .Configure<GameSettings>(configuration)
//                            .AddSingleton<IRepository<GameSettings>>(_ => new JsonRepository<GameSettings>(configFilePath))
//                            .Decorate<IRepository<GameSettings>>(x => new DefaultInitializerRepositoryDecorator<GameSettings>(x, new GameSettings { Audio = { SfxVolume = 0.2f, MusicVolume = 0.2f } }))
//                            .AddSingleton<IEntityFactory, EntityFactory>()
//                            .AddSingleton<IProjectileFactory, ProjectileFactory>()
//                            .AddSingleton<IViewport, Viewport>(_ => new Viewport(0.0f, 0.0f, 3840.0f, 2160.0f))
//                            .AddSingleton<ICamera, Camera>()
//                            .AddMonoGameContentSystem(contentRoot)
//                            .AddMonoGameDrawSystem()
//                            .AddMonoGameAudioSystem(configuration.GetSection("Audio"))
//                            .AddSingleton<IGamePlaySystem, GamePlaySystem>()
//                            .AddSingleton<IGamePlaySystem, OutOfScreenSystem>()
//                            .AddEntitySystem(10, 50)
//                            .AddCollisions(20)
//                            .AddSingleton<IGamePlaySystem, UfoAiSystem>(x => new UfoAiSystem(x.GetRequiredService<IWorld>(), 10))
//                            .AddGameEvents(new[] { Assembly.GetAssembly(typeof(Core.Version)) }, 30)
//                            .AddSingleton<GamePlayContext>()
//                            .AddSingleton<GamePlayScoreManager>();
//                    })
//                .WithConfiguration(config => // TODO: This beast seems become redundant
//                    {
//                        config.FullScreen = false; // TODO: Make as a part of graphics settings?
//                        config.IsMouseVisible = false; // TODO: Make as a part of input settings?
//                        config.ContentPath = "Content"; // TODO: Make as a part of content settings?
//                        config.ScreenColor = Colors.Black; // TODO: Make as a part of graphics settings?
//                    })
//                .Build((services, config) => new MonoGameGame(services, config, new BootstrapScreen<MainMenuScreen>()))
//                .Run();
//        }
    }
}
