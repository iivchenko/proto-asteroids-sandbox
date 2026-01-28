
namespace Engine.Services;

// TODO: It is a bit strange here. I think I need to split low level services and game services
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

    public T RandomPick<T>(params T[] items)
    {
        var index = _random.Next(0, items.Length);

        return items[index];
    }

    public T RandomPick<T>(IEnumerable<T> items)
    {
        var index = _random.Next(0, items.Count());

        return items.ElementAt(index);
    }
}
