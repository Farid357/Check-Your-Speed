using CheckYourSpeed.GameLogic;
using System;
using UniRx;

namespace CheckYourSpeed.Loging
{
    public sealed class SessionsCounterView : TextView
    {
        private ISessionsCounter _sessionsCounter;
        private readonly CompositeDisposable _disposables = new();

        public void Init(ISessionsCounter sessionsCounter)
        {
            _sessionsCounter = sessionsCounter ?? throw new ArgumentNullException(nameof(sessionsCounter));
            _sessionsCounter.Count.Subscribe(count => Display(count)).AddTo(_disposables);
        }

        private void OnDestroy() => _disposables.Dispose();

    }
}