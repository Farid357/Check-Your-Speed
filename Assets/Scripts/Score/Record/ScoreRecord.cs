using UniRx;

namespace CheckYourSpeed.Model
{
    public sealed class ScoreRecord : IDisposable
    {
        private readonly IScore _score;
        private readonly CompositeDisposable _disposables = new();
        private readonly ReactiveProperty<int> _record = new();

        public ScoreRecord(IScore score)
        {
            _score = score ?? throw new System.ArgumentNullException(nameof(score));
            _score.Count.Subscribe(_ => TrySetNew(_)).AddTo(_disposables);
            TrySetNew(_score.Count.Value);
        }

        public IReactiveProperty<int> Record => _record;

        public void Dispose() => _disposables.Dispose();

        private void TrySetNew(int count)
        {
            if (_record.Value < count)
            {
                _record.Value = count;
            }
        }
    }
}
