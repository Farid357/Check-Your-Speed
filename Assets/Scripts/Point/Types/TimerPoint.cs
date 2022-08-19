using System;

namespace CheckYourSpeed.Model
{
    public sealed class TimerPoint : IPoint
    {
        private readonly ITimer _timer;

        public TimerPoint(ITimer timer)
        {
            _timer = timer ?? throw new ArgumentNullException(nameof(timer));
        }

        public event Action<IPoint> OnApplyed;

        public void Apply()
        {
            _timer.Reset();
            OnApplyed?.Invoke(this);
        }
    }
}