using System;
using UniRx;

namespace CheckYourSpeed.Model
{
    public sealed class ScoreRecord : IDisposable
    {
        private readonly IScore _score;
        private readonly ITextView _textView;
        private readonly CompositeDisposable _disposables = new();
        private int _record;

        public ScoreRecord(IScore score, ITextView textView)
        {
            _score = score ?? throw new ArgumentNullException(nameof(score));
            _textView = textView ?? throw new ArgumentNullException(nameof(textView));
            _score.Count.Subscribe(_ => TryIncrease(_)).AddTo(_disposables);
            TryIncrease(_score.Count.Value);
        }

        public void Dispose() => _disposables.Dispose();

        private void TryIncrease(int count)
        {
            if (_record < count)
            {
                _record = count;
                _textView.Visualize(_record);
            }
        }
    }
}
