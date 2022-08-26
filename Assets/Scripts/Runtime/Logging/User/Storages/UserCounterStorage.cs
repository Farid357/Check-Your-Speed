using CheckYourSpeed.SaveSystem;
using CheckYourSpeed.Utils;
using System;

namespace CheckYourSpeed.Loging
{
    public sealed class UserCounterStorage<TStorageUser, TStoredValue, TStorage> : IUserCounterStorage
        where TStorage : IStorage, new()
    {
        private readonly IStorage _storage;
        private readonly IUserWithAccount _user;
        private readonly string _pathName;

        public UserCounterStorage(IUserWithAccount user)
        {
            _storage = new TStorage();
            _user = user ?? throw new ArgumentNullException(nameof(user));
            _pathName = typeof(TStorageUser).Name + typeof(TStoredValue).Name;
        }

        public int Load()
        {
            var data = _storage.Load<UserData>(CreatePath(_user, _pathName));
            return data.Count;
        }

        public void Save(int count)
        {
            var data = new UserData(_user, count);
            _storage.Save(CreatePath(_user, _pathName), data);
        }

        private string CreatePath(IUserWithAccount user, string name) => name + user.Password + user.Name;

        [Serializable]
        private readonly struct UserData
        {
            public readonly IUserWithAccount User;
            public readonly int Count;

            public UserData(IUserWithAccount user, int count)
            {
                User = user ?? throw new ArgumentNullException(nameof(user));
                Count = count.TryThrowLessThanOrEqualsToZeroException();
            }
        }
    }
}