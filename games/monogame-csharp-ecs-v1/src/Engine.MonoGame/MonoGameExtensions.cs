using Engine.Core;
using XVector = Microsoft.Xna.Framework.Vector2;
using XColor = Microsoft.Xna.Framework.Color;

namespace Engine.MonoGame;

public static class MonoGameExtensions
{
    internal static XColor ToXna(this Color color)
    {
        return new XColor(color.Red, color.Green, color.Blue, color.Alpha);
    }

    internal static XVector ToXna(this Vector vector)
    {
        return new XVector(vector.X, vector.Y);
    }
}