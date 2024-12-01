using Engine.Core;

namespace Engine.Services.Draw;

public sealed class Sprite(int height, int width) : Resource
{
    public int Height { get; } = height;
    public int Width { get; } = width;
}
