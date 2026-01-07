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

public partial class MonoGameGame : Game, IGame, IDrawService, IContentService
{
    private SpriteBatch _batch;

    private readonly IServiceProvider _container;
    private readonly Dictionary<Guid, object> _map = [];
    private readonly List<(Sprite, Vector, Core.Color)> _sprites = [];

    public MonoGameGame(IServiceProvider container)
    {
        _container = container;

        Graphics = new GraphicsDeviceManager(this);
    }

    public GraphicsDeviceManager Graphics { get; private set; }

    public IEntryPoint EntryPoint { get; set; }

    protected override void Initialize()
    {
        Content.RootDirectory = "Content";
        IsMouseVisible = true;

        _batch = new SpriteBatch(GraphicsDevice);

        EntryPoint = _container.GetService<IEntryPoint>();

        base.Initialize();
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        EntryPoint.Process((float)gameTime.ElapsedGameTime.TotalSeconds);

        base.Update(gameTime);
    }
}