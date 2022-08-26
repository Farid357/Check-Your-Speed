using System;
using CheckYourSpeed.Utils;

namespace CheckYourSpeed.Model
{
    public sealed class Timer : ITimer, IUpdateble
    {
        private readonly IVisualization<float> _visualization;
        private readonly float _startTime;

        public Timer(float time, IVisualization<float> visualization)
        {
            Time = time.TryThrowLessOrEqualsToZeroException();
            _visualization = visualization ?? throw new ArgumentNullException(nameof(visualization));
            _startTime = Time;
        }

        public float Time { get; private set; }

        public bool FinishedCountdown => Time == 0;

        public void Reset() => Time = _startTime;

        public void Add(float time)
        {
            time.TryThrowLessOrEqualsToZeroException();
            Time += time;
        }

        public void Update(float deltaTime)
        {
            Time = MathF.Max(0, Time - deltaTime);
            _visualization.Visualize(Time);
        }
    }
}