using System;
using System.Collections.Generic;

namespace ProtoAsteroidsGodotCSharp.common
{
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
}
