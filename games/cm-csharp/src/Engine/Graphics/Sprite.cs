using Engine.Assets;

namespace Engine.Graphics;

public sealed class Sprite : Asset
{
    public Sprite(int width,  int height)
    {
        Width = width;
        Height = height;
    }

    public int Width { get; private set; }

    public int Height { get; private set; }
}