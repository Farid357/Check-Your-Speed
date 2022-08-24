using System;
using CheckYourSpeed.Utils;

namespace CheckYourSpeed.Model
{
    public sealed class MultiplePoint : IPoint
    {
        private readonly int _count;
        private readonly IPointsSpawner _pointsSpawner;
        private readonly IPoint _point;

        public MultiplePoint(int count, IPointsSpawner pointsSpawner, IPoint point)
        {
            _count = count.TryThrowLessOrEqualsToZeroException();
            _pointsSpawner = pointsSpawner ?? throw new ArgumentNullException(nameof(pointsSpawner));
            _point = point ?? throw new ArgumentNullException(nameof(point));
        }

        public event Action<IPoint> OnApplied;

        public void Apply()
        {
            _point.Apply();
            OnApplied?.Invoke(this);
            for (int i = 0; i < _count; i++)
            {
                _pointsSpawner.SpawnRandomFrom(new Factory.PointType[] { Factory.PointType.Standart });
            }
        }
    }
}