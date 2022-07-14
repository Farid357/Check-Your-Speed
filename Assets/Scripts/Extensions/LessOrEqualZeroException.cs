using System;

namespace CheckYourSpeed.Utils
{
    public sealed class LessOrEqualZeroException : Exception
    {
        public LessOrEqualZeroException(string message) : base(string.Format("Value is less or equal zero! Value : {0}", message))
        {

        }
    }
}
