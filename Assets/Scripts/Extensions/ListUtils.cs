using System;
using System.Collections.Generic;
using System.Linq;

namespace CheckYourSpeed.Utils
{
    public static class ListUtils
    {
        public static bool HasNotAny<T>(this List<T> list, Func<T, bool> predicate)
        {
            return list.Any(item => predicate.Invoke(item)) == false;
        }

        public static Queue<T> ToQueue<T>(this List<T> list)
        {
            if (list.Count == 0)
                throw new InvalidOperationException(nameof(list));

            var queue = new Queue<T>(list.Count);

            foreach (var item in list)
            {
                queue.Enqueue(item);
            }

            return queue;
        }
    }
}