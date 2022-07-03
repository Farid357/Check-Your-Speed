using CheckYourSpeed.Model;
using System;
using UnityEngine;

namespace CheckYourSpeed.GameLogic
{
    public sealed class Wave
    {
        [SerializeField, Range(1, 4)] private int _pointsCountOnScreen;
        [SerializeReference, SubclassSelector] private IPoint[] _points;
        [SerializeField] private int _pointsCountInWave;
        [SerializeField] private float _delayAfterEnd;

        public Wave(int pointsCountOnScreen, IPoint[] points, int pointsCountInWave, float delayAfterWave)
        {

            _pointsCountOnScreen = pointsCountOnScreen > 0 ? pointsCountOnScreen : throw new ArgumentOutOfRangeException(nameof(pointsCountOnScreen)); ;
            _points = points ?? throw new ArgumentNullException(nameof(points));
            _pointsCountInWave = pointsCountInWave > 0 ? pointsCountInWave : throw new ArgumentOutOfRangeException(nameof(pointsCountInWave));
            _delayAfterEnd = delayAfterWave > 0 ? delayAfterWave : throw new ArgumentOutOfRangeException(nameof(delayAfterWave));
        }

        public Wave() { }

        public int PointsCountOnScreen => _pointsCountOnScreen;
        public IPoint[] Points => _points;
        public int PointsCountInWave => _pointsCountInWave;

        public float DelayAfterEnd => _delayAfterEnd;
    }
}
