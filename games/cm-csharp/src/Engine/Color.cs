namespace Engine;

public sealed class Color(byte red, byte green, byte blue, byte alpha)
{
    public byte Red { get; } = red;
    public byte Green { get; } = green;
    public byte Blue { get; } = blue;
    public byte Alpha { get; } = alpha;

    public static Color operator *(Color value, float scale)
    {
        return new Color((byte)(value.Red * scale), (byte)(value.Green * scale), (byte)(value.Blue * scale), (byte)(value.Alpha * scale));
    }
}
