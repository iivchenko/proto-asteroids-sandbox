namespace Engine.Services;

public sealed class RandomService : IRandomService
{
    private readonly Random _random = new();

    public double NextDouble()
    {
        return _random.NextDouble();
    }

    public int RandomInt(int start, int end)
    {
        return _random.Next(start, end);
    }
}
