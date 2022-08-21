using System;
using CheckYourSpeed.Utils;

namespace CheckYourSpeed.Model
{
    public sealed class RandomPoint : IPoint
    {
        private readonly ITimer _timer;
        private readonly IPointsSwitch _pointsSwitch;

        public RandomPoint(ITimer timer, IPointsSwitch pointsSwitch)
        {
            _timer = timer ?? throw new ArgumentNullException(nameof(timer));
            _pointsSwitch = pointsSwitch ?? throw new ArgumentNullException(nameof(pointsSwitch));
        }

        public event Action<IPoint> OnApplied;

        public void Apply()
        {
          var point =  new Random().GetRandomFromArray<IPoint>(new TimerPoint(_timer), new WavePoint(_pointsSwitch, 
              new TimerPoint(_timer)), new DisablePoint(_pointsSwitch, new TimerPoint(_timer)));
            point.Apply();
            OnApplied?.Invoke(this);
        }
    }
}