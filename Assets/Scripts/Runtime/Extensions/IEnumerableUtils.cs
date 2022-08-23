using System;
using System.Collections.Generic;
using System.Linq;

namespace CheckYourSpeed.Utils
{
    public static class IEnumerableUtils
    {
        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            foreach (var item in enumerable)
            {
                action.Invoke(item);
            }
        }

        public static bool ContainsElementFrom<T>(this List<T> list, IEnumerable<T> enumerable)
        {
            var count = enumerable.Count() > list.Count ? enumerable.Count() : list.Count;

            for (int i = 0; i < count; i++)
            {
                if (list.Contains(enumerable.ElementAt(i)))
                {
                    return true;
                }
            }

            return false;
        }
    }
}