using Engine.Services;
using Raylib_cs;
using System.Numerics;

namespace Engine.Backends.Raylib;

public sealed class RayLibGame_GraphicsService : IGraphicsService, IAssetService<Sprite>
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
            var source = new Rectangle(0, 0, texture.width, texture.height);
            var target = new Rectangle(sprite.Position.X, sprite.Position.Y, texture.width, texture.height);
            var origin = new Vector2(sprite.Origin.X, sprite.Origin.Y);
            var color = new Raylib_cs.Color(sprite.Color.Red, sprite.Color.Green, sprite.Color.Blue, sprite.Color.Alpha); 

            // TODO: Standartize rotation to Degrees or Radians
            Raylib_cs.Raylib.DrawTexturePro(texture, source, target, origin, sprite.Rotation, color);
        }

        Raylib_cs.Raylib.EndDrawing();

        _sprites.Clear();
    }

    public void Draw(Sprite sprite, Vec position, Vec origin, Vec scale, Angle rotation, Color color)
    {
        _sprites.Add(new SpriteDescriptor(sprite, position, origin, scale, rotation.ToDegrees(), color));
    }

    public Sprite Load(string path)
    {
        var texture = Raylib_cs.Raylib.LoadTexture(path);
        var sprite = new Sprite(texture.width, texture.height);

        _textures.Add(sprite.Id, texture);

        return sprite;
    }
}
