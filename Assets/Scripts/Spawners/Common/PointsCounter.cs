using CheckYourSpeed.Model;
using System;
using System.Collections.Generic;

namespace CheckYourSpeed.GameLogic
{
    public sealed class PointsCounter : Model.IDisposable
    {
        private readonly ILoseTimer _loseTimer;
        private readonly IWaveSpawner _waveSpawner;
        private readonly List<IPointView> _allPoints = new();
        private int _count;
        private Wave _currentWave;

        public PointsCounter(IWaveSpawner waveSpawner, ILoseTimer loseTimer)
        {
            _loseTimer = loseTimer ?? throw new ArgumentNullException(nameof(loseTimer));
            _waveSpawner = waveSpawner ?? throw new ArgumentNullException(nameof(waveSpawner));
            _waveSpawner.OnSpawnedNextWave += Count;
        }

        private void Count(Wave wave, IEnumerable<IPointView> spawnedPoints)
        {
            _currentWave = wave;
            var points = spawnedPoints as List<IPointView>;
            points.ForEach(point => point.OnDisabled += Count);
            _allPoints.AddRange(spawnedPoints);
        }

        private void Count() => Count(_currentWave);

        private void Count(Wave wave)
        {
            _count++;

            if (_count >= wave.PointsCountOnScreen)
            {
                _loseTimer.ResetWithAdd(wave.DelayAfterEnd);
                _count = 0;
                _waveSpawner.SpawnWithDelay();
            }
        }

        public void Dispose()
        {
            _allPoints.ForEach(point => point.OnDisabled -= Count);
            _waveSpawner.OnSpawnedNextWave -= Count;
        }
    }
}
