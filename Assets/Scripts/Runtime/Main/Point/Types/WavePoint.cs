using System;

namespace CheckYourSpeed.Model
{
    public sealed class WavePoint : IPoint
    {
        private readonly IPointsSwitch _pointsSwitch;
        private readonly IPoint _point;

        public WavePoint(IPointsSwitch pointsSwitch, IPoint point)
        {
            _pointsSwitch = pointsSwitch ?? throw new ArgumentNullException(nameof(pointsSwitch));
            _point = point ?? throw new ArgumentNullException(nameof(point));
        }

        public event Action<IPoint> OnApplied;

        public void Apply()
        {
            _point.Apply();
            _pointsSwitch.DisableAll();
            OnApplied?.Invoke(this);
        }
    }
}