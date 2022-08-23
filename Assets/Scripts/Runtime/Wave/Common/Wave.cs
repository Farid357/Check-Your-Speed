using CheckYourSpeed.Factory;
using System;
using UnityEngine;

namespace CheckYourSpeed.Model
{
    [Serializable]
    public struct Wave
    {
        [SerializeField, Range(1, 4)] private int _pointsCountOnScreen;
        [SerializeField] private PointType[] _types;
        [SerializeField] private int _pointsCountInWave;
        [SerializeField] private float _delayAfterEnd;

        public Wave(int pointsCountOnScreen, PointType[] points, int pointsCountInWave, float delayAfterWave)
        {
            _pointsCountOnScreen = pointsCountOnScreen > 0 ? pointsCountOnScreen : throw new ArgumentOutOfRangeException(nameof(pointsCountOnScreen)); ;
            _types = points ?? throw new ArgumentNullException(nameof(points));
            _pointsCountInWave = pointsCountInWave > 0 ? pointsCountInWave : throw new ArgumentOutOfRangeException(nameof(pointsCountInWave));
            _delayAfterEnd = delayAfterWave > 0 ? delayAfterWave : throw new ArgumentOutOfRangeException(nameof(delayAfterWave));
        }

        public int PointsCountOnScreen => _pointsCountOnScreen;
        public PointType[] Points => _types;
        public int PointsCountInWave => _pointsCountInWave;

        public float DelayAfterEnd => _delayAfterEnd;
    }
}
