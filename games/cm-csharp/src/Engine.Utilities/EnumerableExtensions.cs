﻿namespace Engine.Utilities;

public static class EnumerableExtensions
{
    private static Random Random = new Random();

    public static void Iter<T>(this IEnumerable<T> source, Action<T> action)
    {
        foreach (var item in source)
        {
            action(item);
        }
    }

    public static T RandomPick<T>(this IEnumerable<T> source)
    {
        var count = source.Count();

        return source.ElementAt(Random.Next(count));
    }
}