using CheckYourSpeed.Model;
using System;

namespace CheckYourSpeed.Factory
{
    public sealed class MiltiplePointFactory : IFactory
    {
        private const int Count = 3;
        private readonly IPointsSpawner _pointsSpawner;
        private readonly ITimer _timer;

        public MiltiplePointFactory(ITimer timer, IPointsSpawner pointsSpawner)
        {
            _pointsSpawner = pointsSpawner ?? throw new ArgumentNullException(nameof(pointsSpawner));
            _timer = timer ?? throw new ArgumentNullException(nameof(timer));
        }

        public IPoint Create()
        {
            return new MultiplePoint(Count, _pointsSpawner, new TimerPoint(_timer));
        }
    }
}
