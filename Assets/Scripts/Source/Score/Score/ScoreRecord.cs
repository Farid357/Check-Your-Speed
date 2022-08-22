using System;
using CheckYourSpeed.Loging;

namespace CheckYourSpeed.Model
{
    public sealed class ScoreRecord : IScoreRecord
    {
        private readonly IVisualization<int> _visualization;
        private readonly IUserCounterStorage _scoreStorage;

        public ScoreRecord(IVisualization<int> visualization, IUserCounterStorage scoreStorage)
        {
            _scoreStorage = scoreStorage ?? throw new ArgumentNullException(nameof(scoreStorage));
            _visualization = visualization ?? throw new ArgumentNullException(nameof(visualization));
            Count = _scoreStorage.Load();
            _visualization.Visualize(Count);
        }
        public int Count { get; private set; }

        public void TryIncrease(int scoreCount)
        {
            if (Count < scoreCount)
            {
                Count = scoreCount;
                _visualization.Visualize(Count);
                _scoreStorage.Save(Count);
            }
        }
    }
}
