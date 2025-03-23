using System.Numerics;

namespace Engine.Math;

// TODO: Move to Engine.Math and split into
// Vector extensions, introduce Angle struct
// and introduce Angle extensions
public static class MathExtensions
{
    public static Vec ToDirection(this float angle)
    {
        var direction = Vector2.Normalize(new Vector2(MathF.Sin(angle), -MathF.Cos(angle)));

        return new Vec(direction.X, direction.Y);
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
