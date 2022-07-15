using CheckYourSpeed.Model;
using UnityEngine;
using UniRx;

namespace CheckYourSpeed.Root
{
    public sealed class ScoreRoot : MonoBehaviour
    {
        [SerializeField] private ScoreRecordView _recordView;
        private readonly CompositeDisposable _disposables = new();
        private IScoreStorage _scoreStorage;
        private Score _score;
        private ScoreRecord _scoreRecord;

        public Score Compose()
        {
            _scoreStorage = new ScoreStorage();
            _score = _scoreStorage.Load() is null ? new() : _scoreStorage.Load();
            _score.Count.Subscribe(_ => SaveScore(_)).AddTo(_disposables);
            var score = _score;
            _scoreRecord = new ScoreRecord(score);
            _recordView.Init(_scoreRecord);
            return _score;
        }

        private void SaveScore(int score) => _scoreStorage.Save(_score);

        private void OnDestroy()
        {
            _disposables.Dispose();
            _scoreRecord.Dispose();
        }
    }
}