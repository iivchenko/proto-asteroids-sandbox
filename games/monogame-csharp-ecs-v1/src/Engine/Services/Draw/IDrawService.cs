using Engine.Core;

namespace Engine.Services.Draw;

public interface IDrawService
{
    public void Draw(Sprite sprite, Vector position, Color color);
}
