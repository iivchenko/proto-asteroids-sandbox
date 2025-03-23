namespace Engine;

public sealed class Angle(float value, AngleType type)
{
    public float Value { get; } = value;
    public AngleType Type { get; } = type; 
}