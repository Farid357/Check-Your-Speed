using System;

namespace CheckYourSpeed.Model
{
    public sealed class ScoreRecord : IUpdateble
    {
        private readonly ITextView _textView;
        private readonly IScore _score;
        private readonly IScoreRecordStorage _scoreStorage;
        private int _count;

        public ScoreRecord(IScore score, ITextView textView, IScoreRecordStorage scoreStorage)
        {
            _score = score ?? throw new ArgumentNullException(nameof(score));
            _scoreStorage = scoreStorage ?? throw new ArgumentNullException(nameof(scoreStorage));
            _textView = textView ?? throw new ArgumentNullException(nameof(textView));
            _count = _scoreStorage.Load();
            _textView.Visualize(_count);
        }

        public void Update(float deltaTime) => TryIncrease();

        private void TryIncrease()
        {
            if (_score.WasCountChanged)
            {
                if (_count < _score.Count)
                {
                    _count = _score.Count;
                    _textView.Visualize(_count);
                    _scoreStorage.Save(_count);
                }
            }
        }
    }
}
