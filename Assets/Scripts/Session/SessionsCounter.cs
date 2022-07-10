using CheckYourSpeed.Model;
using System;
using IDisposable = CheckYourSpeed.Model.IDisposable;
using UniRx;

namespace CheckYourSpeed.Loging
{
    public sealed class SessionsCounter : IDisposable, ISessionsCounter
    {
        private readonly IUser _user;
        private readonly LoseTimer _loseTimer;
        private ReactiveProperty<int> _count = new();
        private bool _hasIncreased;

        public SessionsCounter(LoseTimer loseTimer, IUser user)
        {
            _loseTimer = loseTimer ?? throw new ArgumentNullException(nameof(loseTimer));
            _user = user ?? throw new ArgumentNullException(nameof(user));
            _loseTimer.OnEnded += TryIncrease;
        }

        public IReadOnlyReactiveProperty<int> Count => _count;

        public event Action<int> OnChangedUserData;

        public void SetCount(int count)
        {
            if (_count.Value != 0)
                throw new InvalidOperationException();
            _count.Value = count;
        }

        private void TryIncrease()
        {
            if (_hasIncreased)
                return;

            _hasIncreased = true;
            _count.Value++;
            if (_user.IsAccountable)
            {
                OnChangedUserData?.Invoke(Count.Value);
            }
        }

        public void Dispose() => _loseTimer.OnEnded -= TryIncrease;
    }
}