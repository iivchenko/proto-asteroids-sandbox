namespace Engine;

public sealed class Sprite(int width, int height) : Asset
{
    public int Width { get; private set; } = width;

    public int Height { get; private set; } = height;
}