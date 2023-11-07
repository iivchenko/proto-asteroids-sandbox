using System.Numerics;

namespace Engine.Math;

// TODO: Move to Engine.Math and split into
// Vector extensions, introduce Angle struct
// and introduce Angle extensions
public static class MathExtensions
{
    public static Vector2 ToDirection(this float angle)
    {
        var direction = new Vector2(MathF.Sin(angle), -MathF.Cos(angle));

        return Vector2.Normalize(direction);
    }

    public static float AsRadians(this float angle) // TODO: Think of using this new feature for Math where static interfaces are used
    {
        return (float)((double)angle * (System.Math.PI / 180.0));
    }

    public static float AsRadians(this int angle)
    {
        return (float)(angle * (System.Math.PI / 180.0));
    }
}
