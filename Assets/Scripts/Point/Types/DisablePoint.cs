using System;

namespace CheckYourSpeed.Model
{
    public sealed class DisablePoint : IPoint
    {
        private const int DisableCount = 3;
        private readonly IPointsSwicth _pointsSwicth;
        private readonly IPoint _point;

        public DisablePoint(IPointsSwicth pointsSwicth, IPoint point)
        {
            _pointsSwicth = pointsSwicth ?? throw new ArgumentNullException(nameof(pointsSwicth));
            _point = point ?? throw new ArgumentNullException(nameof(point));
        }

        public event Action<IPoint> OnApplyed;

        public void Apply()
        {
            _point.Apply();
            _pointsSwicth.Disable(DisableCount);
            OnApplyed?.Invoke(this);
        }
    }
}