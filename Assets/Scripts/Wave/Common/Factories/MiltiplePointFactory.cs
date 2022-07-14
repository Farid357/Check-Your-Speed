using CheckYourSpeed.Model;
using System;

namespace CheckYourSpeed.Factory
{
    public sealed class MiltiplePointFactory : IFactory
    {
        private const int Count = 3;
        private readonly IPointsSpawner _pointsSpawner;
        private readonly ILoseTimer _loseTimer;

        public MiltiplePointFactory(ILoseTimer loseTimer, IPointsSpawner pointsSpawner)
        {
            _pointsSpawner = pointsSpawner ?? throw new ArgumentNullException(nameof(pointsSpawner));
            _loseTimer = loseTimer ?? throw new ArgumentNullException(nameof(loseTimer));
        }

        public IPoint Get()
        {
            return new MultiplePoint(_loseTimer, Count, _pointsSpawner);
        }
    }
}
