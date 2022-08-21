using System;
using CheckYourSpeed.Loging;

namespace CheckYourSpeed.Model
{
    public sealed class ScoreRecord : IScoreRecord
    {
        private readonly ITextView _textView;
        private readonly IUserCounterStorage _scoreStorage;

        public ScoreRecord(ITextView textView, IUserCounterStorage scoreStorage)
        {
            _scoreStorage = scoreStorage ?? throw new ArgumentNullException(nameof(scoreStorage));
            _textView = textView ?? throw new ArgumentNullException(nameof(textView));
            Count = _scoreStorage.Load();
            _textView.Visualize(Count);
        }
        public int Count { get; private set; }

        public void TryIncrease(int scoreCount)
        {
            if (Count < scoreCount)
            {
                Count = scoreCount;
                _textView.Visualize(Count);
                _scoreStorage.Save(Count);
            }
        }
    }
}
