namespace Engine.Math;

public static class VecExtensions
{
    public static Vec Normalize(this Vec vec)
    {
        var scalar = 1.0f / MathF.Sqrt((vec.X * vec.X) + (vec.Y * vec.Y));

        return new Vec(vec.X * scalar, vec.Y * scalar);
    }
}
