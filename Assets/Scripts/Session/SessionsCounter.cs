using CheckYourSpeed.Model;
using System;
using IDisposable = CheckYourSpeed.Model.IDisposable;

namespace CheckYourSpeed.Logging
{
    public sealed partial class SessionsCounter : IDisposable
    {
        private readonly IUser _user;
        private readonly LoseTimer _loseTimer;
        private int _count;

        public SessionsCounter(LoseTimer loseTimer, IUser user)
        {
            _loseTimer = loseTimer ?? throw new ArgumentNullException(nameof(loseTimer));
            _user = user ?? throw new ArgumentNullException(nameof(user));
            _loseTimer.OnEnded += Count;
        }

        public event Action<int> OnChanged;

        private event Action<int> _onChangedUserData;

        private void SetCount(int count)
        {
            _count = count;
            OnChanged?.Invoke(_count);
        }

        private void Count()
        {
            _count++;
            if (_user.IsAccountable)
            {
                _onChangedUserData?.Invoke(_count);
            }

            OnChanged?.Invoke(_count);
        }

        public void Dispose() => _loseTimer.OnEnded -= Count;
    }
}