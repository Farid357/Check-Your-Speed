using UnityEngine;
using CheckYourSpeed.Model;
using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace CheckYourSpeed.GameLogic
{
    public sealed class WaveSpawner : MonoBehaviour, IWaveCleaner, IWaveSpawner
    {
        [SerializeField] private float _delay = 0.5f;
        [SerializeField] private Waves _waves;
        [SerializeField] private PointsSpawner _pointsSpawner;
       
        private Wave _currentWave;
        private IPointsSwitch _pointsSwicth;

        public event Action OnSpawningNextWave;
        public event Action<Wave, IEnumerable<IPointView>> OnChangedWave;
        public event Action OnWaiting;
        public event Action OnCleanedWave;

        private IWavesContainer Waves => _waves;

        public void Init(IPointsSwitch pointsSwicth) => _pointsSwicth = pointsSwicth ?? throw new ArgumentNullException(nameof(pointsSwicth));

        public async UniTaskVoid SpawnWithDelay()
        {
            OnWaiting?.Invoke();
            await UniTask.Delay(TimeSpan.FromSeconds(_currentWave.DelayAfterEnd));
            Spawn(true);
        }

        public async UniTaskVoid Spawn(bool needNextWave)
        {
            var wait = new WaitForSeconds(_delay);
            OnSpawningNextWave?.Invoke();
            if (needNextWave)
                _currentWave = Waves.GetNext();

            var count = _currentWave.PointsCountOnScreen;

            for (int i = 0; i < count; i++)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(_delay));
                _pointsSpawner.Spawn(_currentWave.Points);
            }
            OnChangedWave?.Invoke(_currentWave, _pointsSpawner.SpawnedPoints);
        }

        public void CleanWave()
        {
            _pointsSwicth.DisableAll();
            OnCleanedWave?.Invoke();
        }
    }
}
