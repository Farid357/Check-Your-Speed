using System;

namespace CheckYourSpeed.Model
{
    public sealed class DisablePoint : Point
    {
        private const int DisableCount = 3;
        private readonly IPointsSwicth _pointsSwicth;

        public DisablePoint(ILoseTimer timer, IPointsSwicth pointsSwicth) : base(timer)
        {
            _pointsSwicth = pointsSwicth ?? throw new ArgumentNullException(nameof(pointsSwicth));
        }

        protected override void PlayApplyFeedback()
        {
            _pointsSwicth.Disable(DisableCount);
        }
    }
}