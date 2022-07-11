using CheckYourSpeed.SaveSystem;
using System;
using System.Collections.Generic;

namespace CheckYourSpeed.Loging
{
    public sealed class UserLogInStorage : Model.IDisposable
    {
        private readonly IStorage _storage;
        private readonly UserLogIn _userLogIn;

        private const string UsersPath = "UsersAll";

        public UserLogInStorage(IStorage storage, UserLogIn userLogIn)
        {
            _storage = storage ?? throw new ArgumentNullException(nameof(storage));
            _userLogIn = userLogIn ?? throw new ArgumentNullException(nameof(userLogIn));
            var users = _storage.Exists(UsersPath) ? _storage.Load<List<User>>(UsersPath) : new();
            _userLogIn.Init(users);
            _userLogIn.OnChangedData += Save;
        }

        private void Save(List<User> users) => _storage.Save(UsersPath, users);

        public void Dispose() => _userLogIn.OnChangedData -= Save;

    }
}