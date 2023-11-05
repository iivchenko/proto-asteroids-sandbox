namespace Engine.Host.Graphics;

public interface IGraphicsSystem
{
    void Draw(IEnumerable<SpriteDescriptor> sprites);
}
