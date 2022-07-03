using CheckYourSpeed.Model;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace CheckYourSpeed.GameLogic
{
    public sealed class Waves : MonoBehaviour
    {
        [SerializeField] private Queue<Wave> _waves = new();
        [SerializeField] private float _catchTime = 1.5f;
        private Wave _lastWave;

        private bool IsEmpty => _waves.Count > 0;

        private void Awake()
        {
            var timer = new LoseTimer(_catchTime);
            foreach (var wave in _waves)
            {
                foreach (var currentPoint in wave.Points)
                {
                    if (currentPoint is Point point)
                    {
                        point.SetTimer(timer);
                    }
                }
            }
        }
        public Wave Get()
        {
            TryAdd();
            _lastWave = _waves.Peek();
            return _waves.Dequeue();
        }

        public void RemoveFirst()
        {
            if (IsEmpty)
                throw new InvalidOperationException();
            _waves.Dequeue();
        }

        private void TryAdd()
        {
            if (IsEmpty)
            {
                var nextWave = new Wave(_lastWave.PointsCountInWave, _lastWave.Points, _lastWave.PointsCountInWave + 1, _lastWave.DelayAfterEnd);
                _waves.Enqueue(nextWave);
            }
        }
    }
}