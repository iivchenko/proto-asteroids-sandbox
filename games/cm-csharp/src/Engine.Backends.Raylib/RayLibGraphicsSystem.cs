using Engine.Services;
using Raylib_cs;

namespace Engine.Backends.Raylib;

public sealed class RayLibGraphicsSystem : IGraphicsService, IAssetService<Sprite>
{
    private sealed record SpriteDescriptor(
        Sprite Sprite,
        Vec Position,
        Vec Origin,
        Vec Scale,
        float Rotation,
        Color Color);

    private readonly IDictionary<Guid, Texture2D> _textures = new Dictionary<Guid, Texture2D>();
    private readonly List<SpriteDescriptor> _sprites = new List<SpriteDescriptor>();

    public void Commit()
    {
        Raylib_cs.Raylib.BeginDrawing();
        Raylib_cs.Raylib.ClearBackground(Raylib_cs.Color.WHITE);

        foreach (var sprite in _sprites)
        {
            var texture = _textures[sprite.Sprite.Id];

            Raylib_cs.Raylib.DrawTexture(texture, (int)sprite.Position.X, (int)sprite.Position.Y, Raylib_cs.Color.WHITE);
        }

        Raylib_cs.Raylib.EndDrawing();

        _sprites.Clear();
    }

    public void Draw(Sprite sprite, Vec position, Vec origin, Vec scale, float rotation, Color color)
    {
        _sprites.Add(new SpriteDescriptor(sprite, position, origin, scale, rotation, color));
    }

    public Sprite Load(string path)
    {
        var texture = Raylib_cs.Raylib.LoadTexture(path);
        var sprite = new Sprite(texture.width, texture.height);

        _textures.Add(sprite.Id, texture);

        return sprite;
    }
}
