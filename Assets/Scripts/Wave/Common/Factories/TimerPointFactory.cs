using CheckYourSpeed.Model;
using System;

namespace CheckYourSpeed.Factory
{
    public sealed class TimerPointFactory : IFactory
    {
        private readonly ITimer _loseTimer;

        public TimerPointFactory(ITimer loseTimer)
        {
            _loseTimer = loseTimer ?? throw new ArgumentNullException(nameof(loseTimer));
        }

        public IPoint Create()
        {
            return new TimerPoint(_loseTimer);
        }
    }
}
