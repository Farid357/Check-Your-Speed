using System;

namespace CheckYourSpeed.Model
{
    public sealed class DisablePoint : IPoint
    {
        private const int DisableCount = 3;
        private readonly IPointsSwitch _pointsSwicth;
        private readonly IPoint _point;

        public DisablePoint(IPointsSwitch pointsSwicth, IPoint point)
        {
            _pointsSwicth = pointsSwicth ?? throw new ArgumentNullException(nameof(pointsSwicth));
            _point = point ?? throw new ArgumentNullException(nameof(point));
        }

        public event Action<IPoint> OnApplied;

        public void Apply()
        {
            _point.Apply();
            _pointsSwicth.TryDisable(DisableCount);
            OnApplied?.Invoke(this);
        }
    }
}