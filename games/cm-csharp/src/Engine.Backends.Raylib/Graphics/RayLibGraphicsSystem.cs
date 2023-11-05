using Engine.Backends.Raylib.Assets;
using Engine.Game.Graphics;
using Raylib_cs;

namespace Engine.Backends.Raylib.Graphics;

public sealed class RayLibGraphicsSystem : IGraphicsSystem
{
    public readonly RayLibAssetProvider _assets;

    public RayLibGraphicsSystem(RayLibAssetProvider assets)
    {
        _assets = assets;
    }

    public void Draw(IEnumerable<SpriteDescriptor> sprites)
    {
        Raylib_cs.Raylib.BeginDrawing();
        Raylib_cs.Raylib.ClearBackground(Color.WHITE);

        foreach(var sprite in sprites)
        {
            var texture = _assets.Fetch<Texture2D>(sprite.Sprite.Id);

            Raylib_cs.Raylib.DrawTexture(texture, (int)sprite.Position.X, (int)sprite.Position.Y, Raylib_cs.Color.WHITE);
        }

        Raylib_cs.Raylib.EndDrawing();
    }
}
