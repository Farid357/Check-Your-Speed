﻿using System;
using System.Collections.Generic;

namespace CheckYourSpeed.Model
{
    public sealed class Score : IPointsSubscriber
    {
        private readonly ScoreCounter _counter = new();
        private readonly List<IPoint> _points = new();

        public void Subscribe(IPoint point)
        {
            if (point is null)
            {
                throw new ArgumentNullException(nameof(point));
            }

            point.OnApplyed += Add;
            _points.Add(point);
        }

        public event Action<int> OnChanged;

        private int Count => _counter.Score;

        public void Dispose() => _points.ForEach(point => point.OnApplyed -= Add);

        private void Add(IPoint point)
        {
            _counter.Visit((dynamic)point);
            OnChanged?.Invoke(Count);
        }


        private sealed class ScoreCounter : IPointVisitor
        {
            public int Score { get; private set; }

            public void Visit(ScorePoint scorePoint)
            {
                Score += 25;
            }

            public void Visit(WavePoint wavePoint)
            {
                Score += 50;
            }
        }
    }
}