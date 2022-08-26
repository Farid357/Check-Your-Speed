using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CheckYourSpeed.GameLogic
{
    public sealed class PointsPositionSpawner : PointsSpawner
    {
        private IEnumerable<Vector2> _points;
        private int _index;
        
        public void Init(IEnumerable<Vector2> points)
        {
            _points = points ?? throw new ArgumentNullException(nameof(points));
        }

        protected override Vector2 GetSpawnPoint()
        {
            var point = _points.ElementAt(_index);
            _index++;
            return point;
        }
    }
}