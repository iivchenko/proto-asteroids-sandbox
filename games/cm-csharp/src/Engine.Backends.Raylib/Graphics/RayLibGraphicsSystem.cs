using Engine.Assets;
using Engine.Graphics;
using Engine.Host.Graphics;
using Raylib_cs;
using Color = Raylib_cs.Color;

namespace Engine.Backends.Raylib.Graphics;

public sealed class RayLibGraphicsSystem : IGraphicsSystem, IAssetLoader<Sprite>
{
    private readonly IDictionary<Guid, Texture2D> _textures = new Dictionary<Guid, Texture2D>();

    public void Draw(IEnumerable<SpriteDescriptor> sprites)
    {
        Raylib_cs.Raylib.BeginDrawing();
        Raylib_cs.Raylib.ClearBackground(Color.WHITE);

        foreach(var sprite in sprites)
        {
            var texture = _textures[sprite.Sprite.Id];

            Raylib_cs.Raylib.DrawTexture(texture, (int)sprite.Position.X, (int)sprite.Position.Y, Raylib_cs.Color.WHITE);
        }

        Raylib_cs.Raylib.EndDrawing();
    }

    public Sprite Load(string path)
    {
        var texture = Raylib_cs.Raylib.LoadTexture(path);
        var sprite = new Sprite(texture.width, texture.height);

        _textures.Add(sprite.Id, texture);

        return sprite;
    }
}
