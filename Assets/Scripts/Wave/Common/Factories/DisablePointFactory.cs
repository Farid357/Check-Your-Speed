using CheckYourSpeed.Model;
using System;

namespace CheckYourSpeed.Factory
{
    public sealed class DisablePointFactory : IFactory
    {
        private readonly ILoseTimer _loseTimer;
        private readonly IPointsSwicth _pointsSwicth;

        public DisablePointFactory(ILoseTimer loseTimer, IPointsSwicth pointsSwicth)
        {
            _loseTimer = loseTimer ?? throw new ArgumentNullException(nameof(loseTimer));
            _pointsSwicth = pointsSwicth ?? throw new ArgumentNullException(nameof(pointsSwicth));
        }

        public IPoint Get()
        {
            return new DisablePoint(_loseTimer, _pointsSwicth);
        }
    }
}
