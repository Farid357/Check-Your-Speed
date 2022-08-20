using CheckYourSpeed.SaveSystem;
using System;
using System.Collections.Generic;

namespace CheckYourSpeed.Loging
{
    public sealed class UsersStorage
    {
        private readonly IStorage _storage;

        private const string UsersPath = "UsersAll";

        public UsersStorage(IStorage storage)
        {
            _storage = storage ?? throw new ArgumentNullException(nameof(storage));
        }

        public List<User> Load()
        {
            var users = _storage.Exists(UsersPath) ? _storage.Load<List<User>>(UsersPath) : new();
            return users;
        }

        public void Save(List<User> users) => _storage.Save(UsersPath, users);

    }
}