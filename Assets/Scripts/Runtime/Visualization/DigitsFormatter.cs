using System;
using System.Collections.Generic;

namespace CheckYourSpeed.GameLogic
{
    public sealed class DigitsFormatter
    {
        private readonly List<(int, string)> _digitsPrefixes = new()
        {
            (100, "h"),
            (1000, "k"),
            (1000000, "m"),
            (1000000000, "b")
        };

        public string TryFormat(int count)
        {
            var text = count.ToString();
            foreach (var (Digit, Prefix) in _digitsPrefixes)
            {
                if (count < Digit)
                    continue;

                var value = Math.Round(count / (double)Digit, 1);
                text = string.Format("{0}{1}", value, Prefix);
            }

            return text;
        }
    }
}