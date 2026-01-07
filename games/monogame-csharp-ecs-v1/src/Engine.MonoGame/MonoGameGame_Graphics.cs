using Engine.Core;
using Engine.Services.Content;
using Engine.Services.Draw;
using Microsoft.Xna.Framework;

namespace Engine.MonoGame;

public partial class MonoGameGame : Game, IGame, IDrawService, IContentService
{
    public void Draw(Sprite sprite, Vector position, Core.Color color)
    {
        _sprites.Add((sprite, position, color));
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Microsoft.Xna.Framework.Color.Black);

        _batch.Begin();

        foreach (var sprite in _sprites)
        {
            var texture = MapSprite(sprite.Item1);
            _batch
                .Draw(
                    texture,
                    sprite.Item2.ToXna(),
                    sprite.Item3.ToXna());
        }

        _batch.End();

        base.Draw(gameTime);
    }
}
