using CheckYourSpeed.Model;
using UnityEngine;
using UniRx;
using CheckYourSpeed.GameLogic;
using CheckYourSpeed.Utils;
using CheckYourSpeed.SaveSystem;

namespace CheckYourSpeed.Root
{
    public sealed class ScoreRoot : MonoBehaviour
    {
        [SerializeField, RequireInterface(typeof(ITextView))] private MonoBehaviour _recordView;
        [SerializeField, RequireInterface(typeof(IScoreView))] private MonoBehaviour _scoreView;
        private readonly CompositeDisposable _disposables = new();
        private Score _score;
        private IScoreRecordStorage _recordStorage;
        private ScoreRecord _record;

        public Score Compose()
        {
            _score = new(_scoreView as IScoreView);
            _recordStorage = new ScoreRecordStorage(new BinaryStorage());
            _record = new ScoreRecord(_score, _recordView as ITextView, _recordStorage);
            return _score;
        }

        private void OnDestroy() => _disposables.Dispose();

        private void Update()
        {
            _record.Update(Time.deltaTime);
            _score.LateUpdate();
        }
    }
}