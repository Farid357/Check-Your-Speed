using CheckYourSpeed.App;
using System;
using CheckYourSpeed.Utils;

namespace CheckYourSpeed.Model
{
    public sealed class Timer : ITimer, IUpdateble
    {
        private readonly IPauseBroadcaster _pauseBroadcaster;
        private readonly float _startTime;
        private float _time;

        public Timer(float time, IPauseBroadcaster pauseBroadcaster)
        {
            _time = time.TryThrowLessOrEqualZeroException();
            _pauseBroadcaster = pauseBroadcaster ?? throw new ArgumentNullException(nameof(pauseBroadcaster));
            _startTime = _time;
        }

        public bool FinishedCountdown => _time == 0;
        
        public void Reset() => _time = _startTime;

        public void ResetWithAdd(float time) => _time = _startTime + time;

        public void Update(float deltaTime)
        {
            if (_pauseBroadcaster.IsPaused)
                return;

            _time = MathF.Max(0, _time - deltaTime);
        }
    }
}