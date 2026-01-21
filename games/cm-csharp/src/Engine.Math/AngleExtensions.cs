namespace Engine.Math;

public static class AngleExtensions
{
    public static Vec ToVector(this Angle angle)
    {
        var x = MathF.Cos(angle.ToRadians());
        var y = MathF.Sin(angle.ToRadians());
        var scalar = 1.0f / MathF.Sqrt((x * x) + (y * y));

        return new Vec(x * scalar, y * scalar);
    }
}