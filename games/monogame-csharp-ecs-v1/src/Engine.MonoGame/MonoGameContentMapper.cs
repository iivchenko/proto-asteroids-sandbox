using Engine.Core;
using Engine.Services.Draw;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;
using Engine.Services.Content;

namespace Engine.MonoGame;

public sealed class MonoGameContentMapper : IContentService
{
    private readonly ContentManager _content;
    private readonly IDictionary<Guid, object> _map;

    public MonoGameContentMapper(ContentManager content)
    {
        _content = content;
        _map = new Dictionary<Guid, object>();
    }

    public TResource Load<TResource>(string path)
        where TResource : Resource
    {
        var type = typeof(TResource);

        if (type == typeof(Sprite))
        {
            var texture = _content.Load<Texture2D>(path);

            var sprite = new Sprite(texture.Height, texture.Width);

            _map.Add(sprite.Id, texture);

            return sprite as TResource;
        }        
        else
        {
            throw new System.Exception($"Unknown content type {type.Name}!!");
        }
    }

    internal Texture2D MapSprite(Sprite sprite)
    {
        return _map[sprite.Id] as Texture2D;
    }

    internal TContent Load<TContent>(Guid id)
    {
        if (_map.TryGetValue(id, out var value))
        {
            return (TContent)value;
        }

        throw new InvalidOperationException($"Specified id doesn't exist '{id}'!");
    }
}
