using System;

namespace CheckYourSpeed.Utils
{
    public sealed class LessThanOrEqualsToZeroException : Exception
    {
        public LessThanOrEqualsToZeroException(string message) : base(string.Format("Value is less or equal zero! Value : {0}", message))
        {

        }
    }
}
