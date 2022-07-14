using System;
using CheckYourSpeed.Utils;
using Zenject;

namespace CheckYourSpeed.Model
{
    public sealed class RandomPoint : IPoint
    {
        private IWaveCleaner _waveCleaner;
        private ILoseTimer _loseTimer;
        private IPointsSwicth _pointsSwitch;

        public event Action<IPoint> OnApplyed;

        public void Init(ILoseTimer loseTimer, IWaveCleaner waveCleaner)
        {
            _waveCleaner = waveCleaner ?? throw new ArgumentNullException(nameof(waveCleaner));
            _loseTimer = loseTimer ?? throw new ArgumentNullException(nameof(loseTimer));
        }

        [Inject]
        public RandomPoint(IPointsSwicth pointSwitch) => _pointsSwitch = pointSwitch;

        public void Apply()
        {
          var point =  new Random().GetRandomFromArray<Point>(new ScorePoint(_loseTimer), new WavePoint(_loseTimer,
                _waveCleaner), new DisablePoint(_loseTimer, _pointsSwitch));
            point.Apply();
        }
    }
}