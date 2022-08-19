using System;
using CheckYourSpeed.Utils;

namespace CheckYourSpeed.Model
{
    public sealed class MultiplePoint : TimerPoint
    {
        private readonly int _count;
        private readonly IPointsSpawner _pointsSpawner;

        public MultiplePoint(ITimer timer, int count, IPointsSpawner pointsSpawner) : base(timer)
        {
            _count = count.TryThrowLessOrEqualZeroException();
            _pointsSpawner = pointsSpawner ?? throw new ArgumentNullException(nameof(pointsSpawner));
        }

        protected override void PlayApplyFeedback()
        {
            for (int i = 0; i < _count; i++)
            {
                _pointsSpawner.Spawn(new Factory.PointType[] { Factory.PointType.Score });
            }
        }
    }
}