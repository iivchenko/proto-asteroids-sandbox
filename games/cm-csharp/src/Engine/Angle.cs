namespace Engine;

public sealed class Angle
{
    public static readonly Angle Zero = new(0.0f);

    private readonly float _value;

    private Angle(float value)
    {
        _value = value;
    }

    public float ToRadians() => _value;
    public float ToDegrees() => _value * (float)(180.0 / Math.PI);

    public static Angle FromRadians(float radians) => new(radians);
    public static Angle FromDegrees(float degrees) => new(degrees * (float)(Math.PI / 180.0));
    public static Angle Min(Angle angle1, Angle angle2) => new(MathF.Min(angle1._value, angle2._value));
    public static Angle Max(Angle angle1, Angle angle2) => new(MathF.Max(angle1._value, angle2._value));

    public static Angle operator +(Angle angle1, Angle angle2) => new(angle1._value + angle2._value);
    public static Angle operator -(Angle angle1, Angle angle2) => new(angle1._value - angle2._value);
    public static Angle operator *(Angle angle, float scalar) => new(angle._value * scalar);
}