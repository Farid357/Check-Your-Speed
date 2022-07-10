using CheckYourSpeed.SaveSystem;
using System;
using System.Collections.Generic;

namespace CheckYourSpeed.Loging
{
    public sealed class UserLogInStorage : Model.IDisposable, IUserFinder
    {
        private readonly IStorage _storage;
        private readonly UserLogIn _userLogIn;

        private const string UsersPath = "UsersAll";
        private const string LastUser = "UserLasts";


        public UserLogInStorage(IStorage storage, UserLogIn userLogIn)
        {
            _storage = storage ?? throw new ArgumentNullException(nameof(storage));
            _userLogIn = userLogIn ?? throw new ArgumentNullException(nameof(userLogIn));

            var lastUser = _storage.Exists(UsersPath) ? _storage.Load<User>(LastUser) : null;
            var users = _storage.Exists(UsersPath) ? _storage.Load<List<User>>(UsersPath) : new();
            if (lastUser != null)
            {
                OnFoundUser?.Invoke(lastUser);
                _userLogIn.Init(users);
                _userLogIn.OnChangedData += Save;
            }
        }

        public event Action<User> OnFoundUser;

        private void Save(User user, List<User> users)
        {
            _storage.Save(LastUser, user);
            _storage.Save(UsersPath, users);
        }

        public void Dispose() => _userLogIn.OnChangedData -= Save;

    }
}