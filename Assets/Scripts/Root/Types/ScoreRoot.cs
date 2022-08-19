using CheckYourSpeed.Model;
using UnityEngine;
using UniRx;
using CheckYourSpeed.GameLogic;

namespace CheckYourSpeed.Root
{
    public sealed class ScoreRoot : MonoBehaviour
    {
        [SerializeField] private TextView _recordView;
        private readonly CompositeDisposable _disposables = new();
        private IScoreStorage _scoreStorage;
        private IDisposable _scoreRecord;
        private Score _score;

        public Score Compose()
        {
            _scoreStorage = new ScoreStorage();
            _score = _scoreStorage.Load() is null ? new() : _scoreStorage.Load();
            _score.Count.Subscribe(_ => SaveScore(_)).AddTo(_disposables);
            _scoreRecord = new ScoreRecord(_score, _recordView);
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