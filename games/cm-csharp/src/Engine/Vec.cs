namespace Engine;

public sealed class Vec(float x, float y)
{
    public static readonly Vec Zero = new(0, 0);

    public Vec(float value) 
        : this(value, value)
    {
    }

    public float X { get; } = x;
    public float Y { get; } = y;

    public static Vec operator +(Vec vec1, Vec vec2) => new(vec1.X + vec2.X, vec1.Y + vec2.Y);
    public static Vec operator *(Vec vec1, Vec vec2) => new (vec1.X * vec2.X, vec1.Y * vec2.Y);
    public static Vec operator *(Vec vec, float scalar) => new(vec.X * scalar, vec.Y * scalar);

    // TODO: Introduce Normalize method in math extensions
}
