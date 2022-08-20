using System;
using CheckYourSpeed.Utils;

namespace CheckYourSpeed.Model
{
    public sealed class Timer : ITimer, IUpdateble
    {
        private readonly float _startTime;
        private float _time;

        public Timer(float time)
        {
            _time = time.TryThrowLessOrEqualZeroException();
            _startTime = _time;
        }

        public bool FinishedCountdown => _time == 0;
        
        public void Reset() => _time = _startTime;

        public void ResetWithAdd(float time) => _time = _startTime + time;

        public void Update(float deltaTime) => _time = MathF.Max(0, _time - deltaTime);

    }
}