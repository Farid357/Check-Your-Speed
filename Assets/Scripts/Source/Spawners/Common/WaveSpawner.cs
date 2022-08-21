using UnityEngine;
using CheckYourSpeed.Model;
using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using CheckYourSpeed.Utils;

namespace CheckYourSpeed.GameLogic
{
    public sealed class WaveSpawner : MonoBehaviour, IWaveSpawner
    {
        [SerializeField] private float _delay = 0.5f;
        [SerializeField, RequireInterface(typeof(IWavesContainer))] private MonoBehaviour _waves;
        [SerializeField] private PointsSpawner _pointsSpawner;
       
        private Wave _currentWave;
        private IWaveSpawnerView _waveSpawnerView;

        public event Action<Wave, IEnumerable<IPointView>> OnChangedWave;

        public void Init(IWaveSpawnerView waveSpawnerView)
        {
            _waveSpawnerView = waveSpawnerView ?? throw new ArgumentNullException(nameof(waveSpawnerView));
        }

        public async UniTaskVoid SpawnWithDelay()
        {
            _waveSpawnerView.VisualizeWaitingNextWave();
            await UniTask.Delay(TimeSpan.FromSeconds(_currentWave.DelayAfterEnd));
            Spawn(true);
        }

        public async UniTaskVoid Spawn(bool needNextWave)
        {
            var wait = new WaitForSeconds(_delay);
            _waveSpawnerView.VisualizeStartWave();

            if (needNextWave)
                _currentWave = _waves.ToInterface<IWavesContainer>().GetNext();

            var count = _currentWave.PointsCountOnScreen;

            for (int i = 0; i < count; i++)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(_delay));
                _pointsSpawner.SpawnRandomFrom(_currentWave.Points);
            }
            OnChangedWave?.Invoke(_currentWave, _pointsSpawner.SpawnedPoints);
        }
    }
}
