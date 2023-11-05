namespace Engine.Game.Graphics;

public interface IGraphicsSystem
{
    void Draw(IEnumerable<SpriteDescriptor> sprites);
}
