namespace Engine.Services;

public sealed class RandomService : IRandomService
{
    private readonly Random _random = new();

    public int RandomInt(int start, int end)
    {
        return _random.Next(start, end);
    }
}
