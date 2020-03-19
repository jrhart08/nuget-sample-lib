using System;
using System.Collections.Generic;

namespace NdashLib
{
    public static partial class Ndash
    {
        public static void ForEach<T>(this IEnumerable<T> collection, Action<T> action)
        {
            foreach (T item in collection)
            {
                action(item);
            }
        }

        public static void ForEach<T>(this IEnumerable<T> collection, Action<T, int> action)
        {
            int index = 0;
            foreach (T item in collection)
            {
                action(item, index);
                index++;
            }
        }
    }
}
