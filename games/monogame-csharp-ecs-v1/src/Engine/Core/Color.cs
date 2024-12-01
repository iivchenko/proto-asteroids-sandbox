namespace Engine.Core;

public sealed class Color (byte red, byte green, byte blue, byte alpha)
{
    public byte Red { get; } = red;
    public byte Green { get; } = green;
    public byte Blue { get; } = blue;
    public byte Alpha { get; } = alpha;
}
