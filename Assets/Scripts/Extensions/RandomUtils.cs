using System;

namespace CheckYourSpeed.Utils
{
    public static class RandomUtils
    {
        public static T GetRandomFromArray<T>(this Random random, params T[] array)
        {
            if (array is null || array.Length == 0)
                throw new ArgumentNullException(nameof(array));

            var randomIndex = random.Next(0, array.Length);
            return array[randomIndex];
        }
    }
}
