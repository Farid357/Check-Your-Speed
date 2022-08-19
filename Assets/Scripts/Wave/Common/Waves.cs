using CheckYourSpeed.Factory;
using CheckYourSpeed.Model;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace CheckYourSpeed.GameLogic
{
    public sealed class Waves : MonoBehaviour, IWavesContainer
    {
        [SerializeField] private List<Wave> _waves = new();
        private PointFactory _pointFactory;
        private readonly Queue<Wave> _wavesQueue = new();

        private Wave _lastWave;
        private DiContainer _container;

        private bool IsEmpty => _wavesQueue.Count == 0;

        [Inject]
        public void Init(DiContainer container) => _container = container;

        public void Init(ITimer loseTimer, IWaveCleaner waveCleaner)
        {
            _pointFactory = _container.Instantiate<PointFactory>();
            _pointFactory.Init(loseTimer, waveCleaner);
            _waves.ForEach(wave => _wavesQueue.Enqueue(wave));
        }

        public IPoint GetRandomPoint(PointType[] pointTypes)
        {
            var randomIndex = Random.Range(0, pointTypes.Length);
            var randomPointType = pointTypes[randomIndex];
            return _pointFactory.Get(randomPointType);
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
