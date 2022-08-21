using CheckYourSpeed.Model;
using System;

namespace CheckYourSpeed.Factory
{
    public sealed class PointsFactory : IPointsFactory
    {
        private readonly IPointsSpawner _pointsSpawner;
        private readonly IPointsSwitch _pointsSwitch;
        private readonly ITimer _timer;
        private readonly IFactory _randomPointFactory;

        public PointsFactory(ITimer timer, IPointsSwitch pointsSwitch, IPointsSpawner pointsSpawner)
        {
            _timer = timer ?? throw new ArgumentNullException(nameof(timer));
            _pointsSpawner = pointsSpawner ?? throw new ArgumentNullException(nameof(pointsSpawner));
            _pointsSwitch = pointsSwitch ?? throw new ArgumentNullException(nameof(pointsSwitch));
            _randomPointFactory = new RandomPointFactory(_timer, _pointsSwitch);
        }

        public IPoint CreateFrom(PointType type)
        {
            return type switch
            {
                PointType.Wave => new WavePointFactory(_timer, _pointsSwitch).Create(),
                PointType.Standart => new TimerPointFactory(_timer).Create(),
                PointType.Multiple => new MiltiplePointFactory(_timer, _pointsSpawner).Create(),
                PointType.Disable => new DisablePointFactory(_timer, _pointsSwitch).Create(),
                PointType.Random => _randomPointFactory.Create(),
                _ => throw new NotImplementedException(nameof(type))
            };
        }
    }
}
