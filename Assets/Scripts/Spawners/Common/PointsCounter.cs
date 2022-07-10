using CheckYourSpeed.Model;
using CheckYourSpeed.Utils;
using System;
using System.Collections.Generic;
using IDisposable = CheckYourSpeed.Model.IDisposable;

namespace CheckYourSpeed.GameLogic
{
    public sealed class PointsCounter : IDisposable
    {
        private readonly ILoseTimer _loseTimer;
        private readonly WaveSpawner _waveSpawner;
        private readonly List<IPointView> _allPoints = new();
        private int _count;
        private Wave _currentWave;

        public PointsCounter(WaveSpawner waveSpawner, ILoseTimer loseTimer)
        {
            _loseTimer = loseTimer ?? throw new ArgumentNullException(nameof(loseTimer));
            _waveSpawner = waveSpawner ?? throw new ArgumentNullException(nameof(waveSpawner));
            _waveSpawner.OnChangedWave += Count;
            _waveSpawner.OnCleanedWave += Reset;
        }

        private void Count(Wave wave, IEnumerable<IPointView> spawnedPoints)
        {
            _currentWave = wave;
            var points = spawnedPoints as List<IPointView>;
            points.ForEach(point => point.OnDisabled += Count);
            _allPoints.AddRange(spawnedPoints);
        }

        private void Count() => Count(_currentWave);

        private void Reset() => Reset(_currentWave);

        private void Count(in Wave wave)
        {
            _count++;
            if (_count >= wave.PointsCountInWave)
            {
                Reset(wave);
            }

            else if (_count == wave.PointsCountOnScreen || wave.PointsCountOnScreen.InPlusEqual(wave.PointsCountInWave, _count))
            {
                _waveSpawner.Spawn(false);
            }
        }

        private void Reset(in Wave wave)
        {
            _loseTimer.ResetWithAdd(wave.DelayAfterEnd);
            _count = 0;
            _waveSpawner.SpawnWithDelay();
        }

        public void Dispose()
        {
            _allPoints.ForEach(point => point.OnDisabled -= Count);
            _waveSpawner.OnChangedWave -= Count;
            _waveSpawner.OnCleanedWave -= Reset;
        }
    }
}
