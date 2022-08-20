using CheckYourSpeed.Model;
using System;

namespace CheckYourSpeed.Factory
{
    public sealed class DisablePointFactory : IFactory
    {
        private readonly ITimer _timer;
        private readonly IPointsSwitch _pointsSwicth;

        public DisablePointFactory(ITimer timer, IPointsSwitch pointsSwicth)
        {
            _timer = timer ?? throw new ArgumentNullException(nameof(timer));
            _pointsSwicth = pointsSwicth ?? throw new ArgumentNullException(nameof(pointsSwicth));
        }

        public IPoint Create()
        {
            return new DisablePoint(_pointsSwicth, new TimerPoint(_timer));
        }
    }
}
