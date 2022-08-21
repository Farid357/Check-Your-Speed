using System;
using CheckYourSpeed.Loging;

namespace CheckYourSpeed.Model
{
    public sealed class ScoreRecord : IScoreRecord
    {
        private readonly ITextView _textView;
        private readonly IUserCounterStorage _scoreStorage;
        private int _count;

        public ScoreRecord(ITextView textView, IUserCounterStorage scoreStorage)
        {
            _scoreStorage = scoreStorage ?? throw new ArgumentNullException(nameof(scoreStorage));
            _textView = textView ?? throw new ArgumentNullException(nameof(textView));
            _count = _scoreStorage.Load();
            _textView.Visualize(_count);
        }

        public void TryIncrease(int scoreCount)
        {
            if (_count < scoreCount)
            {
                _count = scoreCount;
                _textView.Visualize(_count);
                _scoreStorage.Save(_count);
            }
        }
    }
}
