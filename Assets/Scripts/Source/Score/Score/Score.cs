using System;
using System.Collections.Generic;

namespace CheckYourSpeed.Model
{
    public sealed class Score : IPointsSubscriber
    {
        private readonly IScoreView _view;
        private readonly IScoreRecord _record;
        private readonly ScoreCounter _counter = new();
        private readonly List<IPoint> _points = new();
        private int _count;

        public Score(IScoreView view, IScoreRecord record)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _record = record ?? throw new ArgumentNullException(nameof(record));
        }


        public void Subscribe(IPoint point)
        {
            if (point is null)
            {
                throw new ArgumentNullException(nameof(point));
            }

            point.OnApplied += Add;
            _points.Add(point);
        }

        public void Dispose() => _points.ForEach(point => point.OnApplied -= Add);

        private void Add(IPoint point)
        {
            _counter.Visit((dynamic)point);
            _count = _counter.Score;
            _view.Visualize(_count);
            _record.TryIncrease(_count);
        }

        private sealed class ScoreCounter : IPointVisitor
        {
            public int Score { get; private set; }

            public void Visit(TimerPoint timerPoint) => Score += 25;

            public void Visit(WavePoint wavePoint) => Score += 50;

            public void Visit(DisablePoint disablePoint) => Score += 75;

            public void Visit(MultiplePoint multiplePoint) => Score += 100;

            public void Visit(RandomPoint randomPoint) => Score += 20;

        }
    }
}