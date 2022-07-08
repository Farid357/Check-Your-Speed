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

        public event Action OnSpawningNextWave;
        public event Action<Wave, IEnumerable<IPointView>> OnSpawnedNextWave;
        public event Action OnWaiting;

        private IWavesContainer Waves => _waves;

        public async UniTaskVoid SpawnWithDelay()
        {
            OnWaiting?.Invoke();
            await UniTask.Delay(TimeSpan.FromSeconds(_currentWave.DelayAfterEnd));
            Spawn();
        }

        public async UniTaskVoid Spawn()
        {
            var wait = new WaitForSeconds(_delay);
            OnSpawningNextWave?.Invoke();

            _currentWave = Waves.Get();
            var count = _currentWave.PointsCountOnScreen;

            for (int i = 0; i < count; i++)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(_delay));
                _pointsSpawner.Spawn(_currentWave);
            }

            OnSpawnedNextWave?.Invoke(_currentWave, _pointsSpawner.SpawnedPoints);
        }

        public void CleanWave()
        {
            Waves.RemoveFirst();
            Spawn();
            _pointsSpawner.DisableAll();
        }
    }
}
