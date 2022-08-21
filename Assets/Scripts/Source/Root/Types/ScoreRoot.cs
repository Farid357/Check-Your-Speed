using CheckYourSpeed.Model;
using UnityEngine;
using UniRx;
using CheckYourSpeed.Utils;
using CheckYourSpeed.SaveSystem;
using CheckYourSpeed.Loging;

namespace CheckYourSpeed.Root
{
    public sealed class ScoreRoot : MonoBehaviour
    {
        [SerializeField, RequireInterface(typeof(ITextView))] private MonoBehaviour _recordView;
        [SerializeField, RequireInterface(typeof(IScoreView))] private MonoBehaviour _scoreView;
        private readonly CompositeDisposable _disposables = new();
        private IScoreRecord _record;

        public Score Compose(IUserConfig userConfig)
        {
            IUserCounterStorage recordStorage = new FakeUserCounterStorage();
            if (userConfig.TryLoad(out var userWithAccount))
            {
                recordStorage = new UserCounterStorage(new BinaryStorage(), userWithAccount, "ScoreRecird");
            }

            _record = new ScoreRecord(_recordView.ToInterface<ITextView>(), recordStorage);

            return new Score(_scoreView.ToInterface<IScoreView>(), _record);
        }

        private void OnDestroy() => _disposables.Dispose();

    }
}