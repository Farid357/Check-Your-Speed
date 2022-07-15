using CheckYourSpeed.App;
using System;
using CheckYourSpeed.Utils;

namespace CheckYourSpeed.Model
{
    public sealed class LoseTimer : ILoseTimer, IUpdatable
    {
        private readonly IPauseBroadcaster _pauseBroadcaster;
        private readonly float _startTime;
        private float _time;

        public LoseTimer(float time, IPauseBroadcaster pauseBroadcaster)
        {
            _time = time.TryThrowLessOrEqualZeroException();
            _pauseBroadcaster = pauseBroadcaster ?? throw new ArgumentNullException(nameof(pauseBroadcaster));
            _startTime = _time;
        }

        public event Action OnEnded;

        public void Reset() => _time = _startTime;

        public void ResetWithAdd(float time) => _time = _startTime + time;

        public void Update(float deltaTime)
        {
            if (_pauseBroadcaster.IsPaused)
                return;

            _time -= deltaTime;

            if (_time <= 0)
            {
                OnEnded?.Invoke();
            }
        }
    }
}