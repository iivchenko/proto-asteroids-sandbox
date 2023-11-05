﻿using System.Numerics;

namespace Game;

public static class MathExtensions
{
    public static Vector2 ToDirection(this float angle)
    {
        var direction = new Vector2(MathF.Sin(angle), -MathF.Cos(angle));

        return Vector2.Normalize(direction);
    }

    public static float AsRadians(this float angle)
    {
        return (float)((double)angle * (Math.PI / 180.0));
    }

    public static float AsRadians(this int angle)
    {
        return (float)(angle * (Math.PI / 180.0));
    }
}