using System;
using System.Collections.Generic;

namespace OS.Docker.TestKit
{
    internal static class EnumerableExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> collection, Action<T> action)
        {
            if (collection == null) throw new ArgumentNullException(nameof(collection));

            foreach (var item in collection)
            {
                action?.Invoke(item);
            }
        }
    }
}