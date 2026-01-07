using Engine;
using Engine.Ecs.Components;
using Engine.Ecs.Core;
using Engine.Services.Content;
using System.Collections.Generic;
using System;
using Engine.Services.Draw;
using Engine.Core;

namespace Desktop;

public sealed class GamePlayScene : IScene
{
    private readonly IWorld _world;
    private readonly IContentService _contentService;

    public GamePlayScene(
        IWorld world,
        IContentService contentService)
    {
        _world = world;
        _contentService = contentService;

        var sprite = _contentService.Load<Sprite>("Sprites/PlayerShips/PlayerShip01");

        _world.CreateEntity(
            [
                new SpriteComponent(sprite, new Color(255, 255, 255, 255)),  
                new TransformComponent(new Vector(100, 100))
            ]);
    }

    public void Process(float delta)
    {
        _world.Process(delta);
    }
}
