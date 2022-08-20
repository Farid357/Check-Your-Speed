using CheckYourSpeed.Factory;
using CheckYourSpeed.Model;
using System.Collections.Generic;
using UnityEngine;
using CheckYourSpeed.Utils;

namespace CheckYourSpeed.GameLogic
{
    public sealed class Waves : MonoBehaviour, IWavesContainer
    {
        [SerializeField] private List<Wave> _waves = new();
        private IPointsFactory _pointsFactory;
        private readonly Queue<Wave> _wavesQueue = new();

        private Wave _lastWave;

        private bool IsEmpty => _wavesQueue.Count == 0;

        public void Init(IPointsFactory pointsFactory)
        {
            _pointsFactory = pointsFactory ?? throw new System.ArgumentNullException(nameof(pointsFactory));
            _waves.ForEach(wave => _wavesQueue.Enqueue(wave));
        }

        public IPoint CreateRandomPoint(PointType[] pointTypes)
        {
            var randomPointType = new System.Random().GetRandomFromArray(pointTypes);
            return _pointsFactory.CreateFrom(randomPointType);
        }

        public Wave GetNext()
        {
            TryAdd();
            _lastWave = _wavesQueue.Peek();
            return _wavesQueue.Dequeue();
        }

        private void TryAdd()
        {
            if (IsEmpty)
            {
                var nextWave = new Wave(_lastWave.PointsCountOnScreen, _lastWave.Points, _lastWave.PointsCountInWave + 1, _lastWave.DelayAfterEnd);
                _wavesQueue.Enqueue(nextWave);
            }
        }
    }
}
