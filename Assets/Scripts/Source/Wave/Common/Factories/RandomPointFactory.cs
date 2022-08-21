using CheckYourSpeed.Model;
using System;

namespace CheckYourSpeed.Factory
{
    public sealed class RandomPointFactory : IFactory
    {
        private readonly ITimer _timer;
        private readonly IPointsSwitch _pointsSwitch;

        public RandomPointFactory(ITimer timer, IPointsSwitch pointsSwitch)
        {
            _timer = timer ?? throw new ArgumentNullException(nameof(timer));
            _pointsSwitch = pointsSwitch ?? throw new ArgumentNullException(nameof(pointsSwitch));
        }

        public IPoint Create()
        {
            var randomPoint = new RandomPoint(_timer, _pointsSwitch);
            return randomPoint;
        }
    }
}
