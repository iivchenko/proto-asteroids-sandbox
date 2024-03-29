﻿using System;
using System.Collections.Generic;

public static class EnumerableExtensions
{
    public static void Iter<T>(this IEnumerable<T> collection, Action<T> action)
    {
        foreach (var item in collection)
        {
            action(item);
        }
    }
}

