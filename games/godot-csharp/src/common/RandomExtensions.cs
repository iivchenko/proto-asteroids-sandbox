using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public static class RandomExtensions
{
    public static T Pick<T>(this Random rand, IEnumerable<T> items)
    {
        var count = items.Count();
        var index = rand.Next(count);

        return items.ElementAt(index);
    }
}

