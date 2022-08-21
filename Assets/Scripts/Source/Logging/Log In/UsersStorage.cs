using CheckYourSpeed.SaveSystem;
using System;
using System.Collections.Generic;

namespace CheckYourSpeed.Loging
{
    public sealed class UsersStorage
    {
        private readonly IStorage _storage;

        private const string UsersPath = "UsersStorageAll";

        public UsersStorage(IStorage storage)
        {
            _storage = storage ?? throw new ArgumentNullException(nameof(storage));
        }

        public List<IUserWithAccount> Load()
        {
            var users = _storage.Exists(UsersPath) ? _storage.Load<List<IUserWithAccount>>(UsersPath) : new();
            return users;
        }

        public void Save(List<IUserWithAccount> users)
        {
            if (users is null)
            {
                throw new ArgumentNullException(nameof(users));
            }

            _storage.Save(UsersPath, users);
        }
    }
}