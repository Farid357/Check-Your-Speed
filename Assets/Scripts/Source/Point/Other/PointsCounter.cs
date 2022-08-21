using CheckYourSpeed.Model;
using CheckYourSpeed.Utils;
using System;
using System.Collections.Generic;
using IDisposable = CheckYourSpeed.Model.IDisposable;

namespace CheckYourSpeed.GameLogic
{
    public sealed class PointsCounter : IDisposable
    {
        private readonly ITimer _loseTimer;
        private readonly IPointsSwitch _pointsSwitch;
        private readonly WaveSpawner _waveSpawner;
        private readonly List<IPointView> _allPoints = new();
        private int _count;
        private Wave _currentWave;

        public PointsCounter(WaveSpawner waveSpawner, ITimer loseTimer, IPointsSwitch pointsSwitch)
        {
            _loseTimer = loseTimer ?? throw new ArgumentNullException(nameof(loseTimer));
            _pointsSwitch = pointsSwitch ?? throw new ArgumentNullException(nameof(pointsSwitch));
            _waveSpawner = waveSpawner ?? throw new ArgumentNullException(nameof(waveSpawner));
            _waveSpawner.OnChangedWave += Count;
            _pointsSwitch.OnDisabledAll += Reset;
        }

        private void Count(Wave wave, IEnumerable<IPointView> spawnedPoints)
        {
            _currentWave = wave;
            spawnedPoints.ForEach(point => point.OnDisabled += Count);
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
            _loseTimer.Reset();
            _loseTimer.Add(wave.DelayAfterEnd);
            _count = 0;
            _waveSpawner.SpawnWithDelay();
        }

        public void Dispose()
        {
            _allPoints.ForEach(point => point.OnDisabled -= Count);
            _waveSpawner.OnChangedWave -= Count;
            _pointsSwitch.OnDisabledAll -= Reset;
        }
    }
}
