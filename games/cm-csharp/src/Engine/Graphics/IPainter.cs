using System.Numerics;

namespace Engine.Graphics;

public interface IPainter
{
    void Draw(Sprite sprite, Vector2 position, Vector2 origin, Vector2 scale, float rotation, Color color);
    void Draw(Sprite sprite, Rectangle rectagle, Color color);
    void Draw(Sprite sprite, Rectangle destination, Rectangle source, Color color);
    //void DrawString(Font spriteFont, string text, Vector2 position, Color color);
    //void DrawString(Font spriteFont, string text, Vector2 position, Color color, float rotation, Vector2 origin, float scale);
    void Draw(Sprite sprite, Vector2 position, Rectangle source, Vector2 origin, Vector2 scale, float rotation, Color color);
}

public sealed class Color
{
    public Color(byte red, byte green, byte blue, byte alpha)
    {
        Red = red;
        Green = green;
        Blue = blue;
        Alpha = alpha;
    }

    public byte Red { get; }
    public byte Green { get; }
    public byte Blue { get; }
    public byte Alpha { get; }

    public static Color operator *(Color value, float scale)
    {
        return new Color((byte)(value.Red * scale), (byte)(value.Green * scale), (byte)(value.Blue * scale), (byte)(value.Alpha * scale));
    }
}

public static class Colors
{
    private static readonly Color _white = new Color(255, 255, 255, 255);
    private static readonly Color _turquoise = new Color(64, 224, 208, 255);
    private static readonly Color _black = new Color(0, 0, 0, 255);
    private static readonly Color _red = new Color(255, 0, 0, 255);
    private static readonly Color _yellow = new Color(255, 255, 0, 255);
    private static readonly Color _blue = new Color(0, 0, 255, 255);
    private static readonly Color _darkGray = new Color(169, 169, 169, 255);

    public static Color White => _white;
    public static Color Turquoise => _turquoise;
    public static Color Black => _black;
    public static Color Red => _red;
    public static Color Yellow => _yellow;
    public static Color Blue => _blue;
    public static Color DarkGray => _darkGray;
}

public struct Rectangle
{
    public Rectangle(int x, int y, int width, int height)
    {
        X = x;
        Y = y;
        Width = width;
        Height = height;
    }

    public int X { get; set; }
    public int Y { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
}