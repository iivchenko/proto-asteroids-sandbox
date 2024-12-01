using Engine.Core;
using Engine.Services.Draw;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Engine.MonoGame;

public sealed class MonoGameDrawService : IDrawService
{
    private readonly SpriteBatch _batch;
    private readonly MonoGameContentMapper _mapper;

    private readonly List<(Sprite, Vector, Color)> _sprites;

    public MonoGameDrawService(
        SpriteBatch batch,
        MonoGameContentMapper mapper)
    {
        _batch = batch;
        _mapper = mapper;

        _sprites = new List<(Sprite, Vector, Color)>();
    }

    public void Begin()
    {
        _batch.Begin();
    }

    public void End()
    {
        foreach (var sprite in _sprites)
        {
            var texture = _mapper.MapSprite(sprite.Item1);
            _batch
                .Draw(
                    texture,
                    sprite.Item2.ToXna(),
                    sprite.Item3.ToXna());
        }

        _batch.End();
    }
    public void Draw(Sprite sprite, Vector position, Color color)
    {
        _sprites.Add((sprite, position, color));        
    }
}
