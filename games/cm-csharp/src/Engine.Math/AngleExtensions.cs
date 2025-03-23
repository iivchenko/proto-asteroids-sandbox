namespace Engine.Math;

public static class AngleExtensions
{
    public static Angle ToRadians(this Angle angle)
    {
        return angle.Type switch
        {
            AngleType.Degrees => new Angle((float)(angle.Value * (System.Math.PI / 180.0)), AngleType.Radians),
            _ => angle
        };
    }

    public static Angle ToDegrees(this Angle angle)
    {
        return angle.Type switch
        {
            AngleType.Radians => new Angle((float)(angle.Value * (180.0/ System.Math.PI)), AngleType.Radians),
            _ => angle
        };
    }

    public static Vec ToVector(this Angle angle)
    {
        var value = angle.Type switch
        {
            AngleType.Radians => angle.ToDegrees().Value,
            _ => angle.Value
        };


        var x = MathF.Sin(value);
        var y = -MathF.Cos(value);
        var scalar = 1.0f / MathF.Sqrt((x * x) + (y * y));

        return new Vec(x * scalar, y * scalar);
    }
}