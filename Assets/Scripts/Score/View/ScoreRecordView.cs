using CheckYourSpeed.GameLogic;
using UniRx;

namespace CheckYourSpeed.Model
{
    public sealed class ScoreRecordView : TextView
    {
        private readonly CompositeDisposable _disposables = new();
        private ScoreRecord _scoreRecord;

        public void Init(ScoreRecord scoreRecord)
        {
            _scoreRecord = scoreRecord ?? throw new System.ArgumentNullException(nameof(scoreRecord));
            _scoreRecord.Record.Subscribe(_ => Display(_)).AddTo(_disposables);
        }

        private void OnDestroy() => _disposables.Dispose();

    }
}
