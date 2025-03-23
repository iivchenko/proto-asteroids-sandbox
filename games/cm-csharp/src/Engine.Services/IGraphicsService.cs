using System.Numerics;

namespace Engine.Services;

public interface IGraphicsService
{
    void Draw(Sprite sprite, Vec position, Vec origin, Vec scale, float rotation, Color color);
}
