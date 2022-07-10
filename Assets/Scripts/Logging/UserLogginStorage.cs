using CheckYourSpeed.Model;
using CheckYourSpeed.SaveSystem;
using System;
using System.Collections.Generic;

namespace CheckYourSpeed.Logging
{
    public partial class UserLoggIn
    {
        public sealed class UserLogginStorage : Model.IDisposable
        {
            private readonly IStorage _storage;
            private readonly UserLoggIn _userLoggIn;

            private const string UsersPath = "Users";
            private const string LastUser = "UserLast";

            public UserLogginStorage(IStorage storage, UserLoggIn userLoggIn)
            {
                _storage = storage ?? throw new ArgumentNullException(nameof(storage));
                _userLoggIn = userLoggIn ?? throw new ArgumentNullException(nameof(userLoggIn));
                _userLoggIn._users = _storage.Load<List<User>>(UsersPath);
                _userLoggIn._lastUser = _storage.Load<User>(LastUser);
                _userLoggIn._onChangedData += Save;
            }

            private void Save(User user, List<User> users)
            {
                _storage.Save(LastUser, user);
                _storage.Save(UsersPath, users);
            }

            public void Dispose() => _userLoggIn._onChangedData -= Save;

        }
    }
}