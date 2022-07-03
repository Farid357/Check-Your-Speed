using System;

namespace CheckYourSpeed.Model
{
    [Serializable]
    public abstract class Point : IPoint
    {
        private LoseTimer _timer;

        public void SetTimer(LoseTimer timer)
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