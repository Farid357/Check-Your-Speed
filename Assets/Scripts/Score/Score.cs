using System;
using System.Collections.Generic;
using UniRx;

namespace CheckYourSpeed.Model
{
    public sealed class Score : IPointsSubscriber
    {
        private readonly ScoreCounter _counter = new();
        private readonly List<IPoint> _points = new();
        private readonly ReactiveProperty<int> _count = new();

        public void Subscribe(IPoint point)
        {
            if (point is null)
            {
                throw new ArgumentNullException(nameof(point));
            }

            point.OnApplyed += Add;
            _points.Add(point);
        }

        public IReadOnlyReactiveProperty<int> Count => _count;

        public void Dispose() => _points.ForEach(point => point.OnApplyed -= Add);

        private void Add(IPoint point)
        {
            _counter.Visit((dynamic)point);
            _count.Value = _counter.Score;
        }


        private sealed class ScoreCounter : IPointVisitor
        {
            public int Score { get; private set; }

            public void Visit(ScorePoint scorePoint) => Score += 25;

            public void Visit(WavePoint wavePoint) => Score += 50;

            public void Visit(DisablePoint disablePoint) => Score += 75;

            public void Visit(MultiplePoint multiplePoint) => Score += 100;

        }
    }
}