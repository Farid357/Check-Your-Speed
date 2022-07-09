using CheckYourSpeed.Model;
using CheckYourSpeed.SaveSystem;
using System;

namespace CheckYourSpeed.Logging
{
    public sealed class SessionsCounter : Model.IDisposable
    {
        private readonly IStorage _storage;
        private readonly IUser _user;
        private readonly LoseTimer _loseTimer;
        private int _count;
        private const string Path = "Attempts";

        public SessionsCounter(LoseTimer loseTimer, IStorage storage, IUser user)
        {
            _loseTimer = loseTimer ?? throw new ArgumentNullException(nameof(loseTimer));
            _storage = storage ?? throw new ArgumentNullException(nameof(storage));
            _user = user ?? throw new ArgumentNullException(nameof(user));
            _count = _storage.Load<int>(Path);
            _loseTimer.OnEnded += Count;
        }

        public event Action<int> OnChanged;

        private void Count()
        {
            _count++;
            if (_user.IsAccountable)
            {
                _storage.Save(Path, _count);
            }

            OnChanged?.Invoke(_count);
        }

        public void Dispose() => _loseTimer.OnEnded -= Count;

    }
}