using Engine.Core;
using Engine.Services.Content;
using Engine.Services.Draw;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Engine.MonoGame;

public class MonoGameGame : Game, IGame, IDrawService, IContentService
{
    private SpriteBatch _batch;
    private readonly IServiceProvider _container;
    private readonly IDictionary<Guid, object> _map;
    private readonly List<(Sprite, Vector, Core.Color)> _sprites;

    public MonoGameGame(IServiceProvider container)
    {
        _container = container;
        _map = new Dictionary<Guid, object>();
        _sprites = new List<(Sprite, Vector, Core.Color)>();
        Graphics = new GraphicsDeviceManager(this);
    }

    public GraphicsDeviceManager Graphics { get; private set; }

    public IEntryPoint EntryPoint { get; set; }

    public void Draw(Sprite sprite, Vector position, Core.Color color)
    {
        _sprites.Add((sprite, position, color));
    }

    public TResource Load<TResource>(string path)
    where TResource : Resource
    {
        var type = typeof(TResource);

        if (type == typeof(Sprite))
        {
            var texture = Content.Load<Texture2D>(path);

            var sprite = new Sprite(texture.Height, texture.Width);

            _map.Add(sprite.Id, texture);

            return sprite as TResource;
        }
        else
        {
            throw new System.Exception($"Unknown content type {type.Name}!!");
        }
    }

    private Texture2D MapSprite(Sprite sprite)
    {
        return _map[sprite.Id] as Texture2D;
    }

    private TContent Load<TContent>(Guid id)
    {
        if (_map.TryGetValue(id, out var value))
        {
            return (TContent)value;
        }

        throw new InvalidOperationException($"Specified id doesn't exist '{id}'!");
    }

    protected override void Initialize()
    {
        Content.RootDirectory = "Content";
        IsMouseVisible = true;

        _batch = new SpriteBatch(GraphicsDevice);

        EntryPoint = _container.GetService<IEntryPoint>();

        base.Initialize();
    }

    protected override void LoadContent()
    {
        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        EntryPoint.Process((float)gameTime.ElapsedGameTime.TotalSeconds);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Microsoft.Xna.Framework.Color.Black);

        _batch.Begin();
        
        foreach (var sprite in _sprites)
        {
            var texture = MapSprite(sprite.Item1);
            _batch
                .Draw(
                    texture,
                    sprite.Item2.ToXna(),
                    sprite.Item3.ToXna());
        }

        _batch.End();

        base.Draw(gameTime);
    }
}
