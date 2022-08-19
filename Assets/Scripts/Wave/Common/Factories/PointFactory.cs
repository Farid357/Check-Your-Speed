using CheckYourSpeed.Model;
using CheckYourSpeed.Utils;
using System;
using Zenject;

namespace CheckYourSpeed.Factory
{
    public sealed class PointFactory
    {
        private readonly IPointsSpawner _pointsSpawner;
        private readonly IPointsSwicth _pointsSwitch;
        private IWaveCleaner _waveCleaner;
        private ITimer _loseTimer;
        private IFactory _factory;
        private RandomPointFactory _randomPointFactory;
        private DiContainer _container;

        public void Init(ITimer loseTimer, IWaveCleaner waveCleaner)
        {
            _waveCleaner = waveCleaner ?? throw new ArgumentNullException(nameof(waveCleaner));
            _loseTimer = loseTimer ?? throw new ArgumentNullException(nameof(loseTimer));
            _randomPointFactory = new RandomPointFactory(_loseTimer, _waveCleaner, _container);
        }

        [Inject]
        public PointFactory(IPointsSwicth pointSwitch, IPointsSpawner pointsSpawner, DiContainer container)
        {
            (_pointsSpawner, _pointsSwitch, _container) = (pointsSpawner, pointSwitch, container);
        }

        public IPoint Get(PointType type)
        {
            switch (type)
            {
                case PointType.Wave:
                    _factory = new WavePointFactory(_loseTimer, _waveCleaner);
                    return _factory.Get();
                case PointType.Score:
                    _factory = new ScorePointFactory(_loseTimer);
                    return _factory.Get();
                case PointType.Multiple:
                    _factory = new MiltiplePointFactory(_loseTimer, _pointsSpawner);
                    return _factory.Get();
                case PointType.Disable:
                    _factory = new DisablePointFactory(_loseTimer, _pointsSwitch);
                    return _factory.Get();
                case PointType.Random:
                    _factory = _randomPointFactory;
                    return _factory.Get();
                default:
                    throw new InvalidOperationException(nameof(type));
            }
        }
    }
}
