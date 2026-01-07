using Engine.Services;

namespace Engine.Backends.Sfml;

public sealed partial class SfmlGame : IGraphicsService, IAssetService<Sprite>
{
    private sealed record SpriteDescriptor(
        Sprite Sprite,
        Vec Position,
        Vec Origin,
        Vec Scale,
        float Rotation,
        Color Color);

    private readonly IDictionary<Guid, SFML.Graphics.Sprite> _textures = new Dictionary<Guid, SFML.Graphics.Sprite>();
    private readonly List<SpriteDescriptor> _sprites = [];

    private void Display()
    {
        _window.Clear(SFML.Graphics.Color.White);

        foreach (var sprite in _sprites)
        {
            var texture = _textures[sprite.Sprite.Id];

            texture.Position = new SFML.System.Vector2f(sprite.Position.X, sprite.Position.Y);

            _window.Draw(texture);
        }

        _window.Display();
        _sprites.Clear();
    }

    public void Draw(Sprite sprite, Vec position, Vec origin, Vec scale, float rotation, Color color)
    {
        _sprites.Add(new SpriteDescriptor(sprite, position, origin, scale, rotation, color));
    }

    public Sprite Load(string path)
    {   
        var texture = new SFML.Graphics.Sprite(new SFML.Graphics.Texture(path));
        var sprite = new Sprite((int)texture.Texture.Size.X, (int)texture.Texture.Size.Y);

        _textures.Add(sprite.Id, texture);

        return sprite;
    }
}