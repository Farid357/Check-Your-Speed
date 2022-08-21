using System;
using CheckYourSpeed.Utils;

namespace CheckYourSpeed.Model
{
    public sealed class Timer : ITimer, IUpdateble
    {
        private readonly float _startTime;

        public Timer(float time)
        {
            Time = time.TryThrowLessOrEqualZeroException();
            _startTime = Time;
        }

        public float Time { get; private set; }

        public bool FinishedCountdown => Time == 0;

        public void Reset() => Time = _startTime;

        public void Add(float time)
        {
            time.TryThrowLessOrEqualZeroException();
            Time += time;
        }

        public void Update(float deltaTime) => Time = MathF.Max(0, Time - deltaTime);

    }
}