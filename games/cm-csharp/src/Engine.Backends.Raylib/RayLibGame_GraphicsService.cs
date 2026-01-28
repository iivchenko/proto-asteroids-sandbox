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

    private readonly Dictionary<Guid, Texture2D> _textures = [];
    private readonly List<SpriteDescriptor> _sprites = [];

    public void Commit()
    {
        Raylib_cs.Raylib.BeginDrawing();
        Raylib_cs.Raylib.ClearBackground(Raylib_cs.Color.White);

        foreach (var sprite in _sprites)
        {
            var texture = _textures[sprite.Sprite.Id];
            var source = new Rectangle(0, 0, texture.Width, texture.Height);
            var target = new Rectangle(sprite.Position.X, sprite.Position.Y, texture.Width, texture.Height);
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
        var sprite = new Sprite(texture.Width, texture.Height);

        _textures.Add(sprite.Id, texture);

        return sprite;
    }
}
