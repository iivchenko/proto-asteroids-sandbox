using Engine.Core;
using Engine.Services.Content;
using Engine.Services.Draw;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;

namespace Engine.MonoGame;

public partial class MonoGameGame : Game, IGame, IDrawService, IContentService
{
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
}