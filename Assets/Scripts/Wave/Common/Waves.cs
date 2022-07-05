using CheckYourSpeed.Factory;
using CheckYourSpeed.Model;
using System.Collections.Generic;
using UnityEngine;

namespace CheckYourSpeed.GameLogic
{
    public sealed class Waves : MonoBehaviour, IWavesContainer
    {
        [SerializeField] private List<Wave> _waves = new();
        private PointFactory _pointFactory;
        private readonly Queue<Wave> _wavesQueue = new();

        private Wave _lastWave;

        private bool IsEmpty => _wavesQueue.Count == 0;

        public void Init(ILoseTimer loseTimer, IWaveCleaner waveCleaner)
        {
            _pointFactory = new(loseTimer, waveCleaner);
            _waves.ForEach(wave => _wavesQueue.Enqueue(wave));
        }

        public IPoint GetRandomPoint(Wave wave)
        {
            var randomIndex = Random.Range(0, wave.Points.Length);
            var randomPointType = wave.Points[randomIndex];
            return _pointFactory.Get(randomPointType);
        }

        public Wave Get()
        {
            TryAdd();
            _lastWave = _wavesQueue.Peek();
            return _wavesQueue.Dequeue();
        }

        public void RemoveFirst()
        {
            TryAdd();
            _wavesQueue.Dequeue();
        }

        private void TryAdd()
        {
            if (IsEmpty)
            {
                var nextWave = new Wave(_lastWave.PointsCountInWave, _lastWave.Points, _lastWave.PointsCountInWave + 1, _lastWave.DelayAfterEnd);
                _wavesQueue.Enqueue(nextWave);
            }
        }
    }
}
