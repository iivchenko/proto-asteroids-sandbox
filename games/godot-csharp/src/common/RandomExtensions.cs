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

    public static Vector2 NextDirection(this Random rand)
    {
        var xSign = rand.Next(0, 2) == 0 ? 1 : -1;
        var ySign = rand.Next(0, 2) == 0 ? 1 : -1;
        var x = rand.NextDouble() * xSign;
        var y = rand.NextDouble() * ySign;

        return new Vector2((float)x, (float)y).Normalized();
    }
}

