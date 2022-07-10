using System;

namespace CheckYourSpeed.Loging
{
    public interface ILoging
    {
        public event Action OnFoundInvalidSymbols;
        public event Action OnWrote;
    }
}