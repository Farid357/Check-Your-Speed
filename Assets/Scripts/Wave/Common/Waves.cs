using CheckYourSpeed.Model;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace CheckYourSpeed.GameLogic
{
    public sealed class Waves : MonoBehaviour, IWavesContainer
    {
        [SerializeField] private List<Wave> _waves = new();

        private readonly Queue<Wave> _wavesQueue = new();
        private Wave _lastWave;

        private bool IsEmpty => _wavesQueue.Count == 0;

        public void Init(ILoseTimer loseTimer)
        {
            foreach (var wave in _waves)
            {
                _wavesQueue.Enqueue(wave);
                foreach (var currentPoint in wave.Points)
                {
                    if (currentPoint is Point point)
                    {
                        point.SetTimer(loseTimer);
                    }
                }
            }
        }

        public Wave Get()
        {
            TryAdd();
            _lastWave = _wavesQueue.Peek();
            return _wavesQueue.Dequeue();
        }

        public void RemoveFirst()
        {
            if (IsEmpty)
                throw new InvalidOperationException();
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