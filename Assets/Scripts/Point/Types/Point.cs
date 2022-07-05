using System;

namespace CheckYourSpeed.Model
{
    public abstract class Point : IPoint
    {
        private ILoseTimer _timer;

        public Point(ILoseTimer timer)
        {
            _timer = timer ?? throw new ArgumentNullException(nameof(timer));
        }

        public event Action<IPoint> OnApplyed;

        public void Apply()
        {
            _timer.Reset();
            PlayApplyFeedback();
            OnApplyed?.Invoke(this);
        }

        protected abstract void PlayApplyFeedback();
    }
}