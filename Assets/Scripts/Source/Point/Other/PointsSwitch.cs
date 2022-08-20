using System;
using System.Collections.Generic;
using System.Linq;

namespace CheckYourSpeed.Model
{
    public sealed class PointsSwitch : IPointsSwitch, IPointsContainer
    {
        private readonly List<IPointView> _points = new();

        private List<IPointView> _enablePoints => _points.Where(point => point.Enable).ToList();

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

        public void Add(IPointView point) => _points.Add(point);

    }
}
