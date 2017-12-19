using System;
using System.Collections.Generic;

namespace OS.DAL
{
    internal static class InternalExtensions
    {
        public static void ForEach<T>(this IList<T> collection, Action<T> action)
        {
            if (collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            foreach (var item in collection)
            {
                action(item);
            }
        }
    }
}