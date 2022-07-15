using System;
using System.Collections.Generic;
using System.Linq;

namespace CheckYourSpeed.Model
{
    public sealed class PointsSwitch : IDisposable, IPointsSwicth
    {
        private readonly IPointsSpawner _pointsSpawner;
        private readonly List<IPointView> _points = new();

        public PointsSwitch(IPointsSpawner pointsSpawner)
        {
            _pointsSpawner = pointsSpawner ?? throw new ArgumentNullException(nameof(pointsSpawner));
            _pointsSpawner.OnSpawned += Add;
        }

        private List<IPointView> _enablePoints => _points.Where(point => point.Enable).ToList();

        public void Dispose() => _pointsSpawner.OnSpawned -= Add;

        public void DisableAll() => _enablePoints.ForEach(point => point.Disable());

        public void Disable(int count)
        {
            try
            {
                for (int i = 0; i < count; i++)
                {
                    _enablePoints[i].Disable();
                }
            }

            catch (Exception) { }        
        }

        private void Add(IPointView point) => _points.Add(point);

    }
}
