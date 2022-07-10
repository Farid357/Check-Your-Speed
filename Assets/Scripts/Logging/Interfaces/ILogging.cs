using System;

namespace CheckYourSpeed.Logging
{
    public interface ILogging
    {
        public event Action OnFoundInvalidSymbols;
        public event Action OnLogged;
    }
}