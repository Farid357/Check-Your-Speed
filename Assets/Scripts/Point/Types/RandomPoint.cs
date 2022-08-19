using System;
using CheckYourSpeed.Utils;
using Zenject;

namespace CheckYourSpeed.Model
{
    public sealed class RandomPoint : IPoint
    {
        private readonly IWaveCleaner _waveCleaner;
        private readonly ITimer _timer;
        private readonly IPointsSwicth _pointsSwitch;

        public event Action<IPoint> OnApplyed;

        [Inject]
        public RandomPoint(IPointsSwicth pointSwitch) => _pointsSwitch = pointSwitch;

        public RandomPoint(IWaveCleaner waveCleaner, ITimer timer)
        {
            _waveCleaner = waveCleaner ?? throw new ArgumentNullException(nameof(waveCleaner));
            _timer = timer ?? throw new ArgumentNullException(nameof(timer));
        }

        public void Apply()
        {
          var point =  new Random().GetRandomFromArray<IPoint>(new TimerPoint(_timer), new WavePoint(_waveCleaner, 
              new TimerPoint(_timer)), new DisablePoint(_pointsSwitch, new TimerPoint(_timer)));
            point.Apply();
        }
    }
}