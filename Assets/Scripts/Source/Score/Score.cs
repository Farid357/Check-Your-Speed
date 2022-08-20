using System;
using System.Collections.Generic;

namespace CheckYourSpeed.Model
{
    public sealed class Score : IPointsSubscriber, IScore, ILateUpdateble
    {
        private readonly ScoreCounter _counter = new();
        private readonly List<IPoint> _points = new();
        private readonly IScoreView _scoreView;

        public Score(IScoreView scoreView)
        {
            _scoreView = scoreView ?? throw new ArgumentNullException(nameof(scoreView));
        }

        public bool WasCountChanged { get; private set; }

        public int Count { get; private set; }

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
            Count = _counter.Score;
            _scoreView.Visualize(Count);
            WasCountChanged = true;
        }

        public void LateUpdate() => WasCountChanged = false;

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