using System;
using System.Collections.Generic;
using System.Linq;

namespace CheckYourSpeed.Utils
{
    public static class ListUtils
    {
        public static bool HasNotAny<T>(this List<T> list, Func<T, bool> predicate)
        {
            return list.Any(item => predicate.Invoke(item) == false);
        }
    }
}