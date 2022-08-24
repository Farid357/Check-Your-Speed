using System;

namespace CheckYourSpeed.Utils
{
    public sealed class LessOrEqualsToZeroException : Exception
    {
        public LessOrEqualsToZeroException(string message) : base(string.Format("Value is less or equal zero! Value : {0}", message))
        {

        }
    }
}
