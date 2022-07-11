using CheckYourSpeed.Model;
using System;
using IDisposable = CheckYourSpeed.Model.IDisposable;
using UniRx;

namespace CheckYourSpeed.Loging
{
    public sealed class SessionsCounter : IDisposable, ISessionsCounter
    {
        private readonly IUser _user;
        private readonly SessionsCounterStorage _storage;
        private readonly LoseTimer _loseTimer;
        private ReactiveProperty<int> _count = new();
        private bool _hasIncreased;

        public SessionsCounter(LoseTimer loseTimer, IUser user, SessionsCounterStorage storage)
        {
            _loseTimer = loseTimer ?? throw new ArgumentNullException(nameof(loseTimer));
            _user = user ?? throw new ArgumentNullException(nameof(user));
            _storage = storage ?? throw new ArgumentNullException(nameof(storage));
            _count.Value = storage.Load();
            _loseTimer.OnEnded += TryIncrease;
        }

        public IReadOnlyReactiveProperty<int> Count => _count;

        private void TryIncrease()
        {
            if (_hasIncreased)
                return;

            _hasIncreased = true;
            _count.Value++;
            if (_user.IsAccountable)
            {
               _storage.Save(_user, Count.Value);
            }
        }

        public void Dispose() => _loseTimer.OnEnded -= TryIncrease;
    }
}