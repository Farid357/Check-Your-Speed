using CheckYourSpeed.SaveSystem;
using CheckYourSpeed.Utils;
using System;

namespace CheckYourSpeed.Loging
{
    public sealed class UserCounterStorage : IUserCounterStorage
    {
        private readonly IStorage _storage;
        private readonly IUserWithAccount _user;
        private readonly string _path = "AttemptCount";

        public UserCounterStorage(IStorage storage, IUserWithAccount user, string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentException($"\"{nameof(path)}\" не может быть пустым или содержать только пробел.", nameof(path));
            }

            _storage = storage ?? throw new ArgumentNullException(nameof(storage));
            _user = user ?? throw new ArgumentNullException(nameof(user));
            _path = path;
        }

        public int Load()
        {
            var data = _storage.Load<UserData>(_path + _user.Password + _user.Name);
            return data.Count;
        }

        public void Save(int count)
        {
            var data = new UserData(_user, count);
            _storage.Save(_path + _user.Password + _user.Name, data);
        }

        [Serializable]
        private readonly struct UserData
        {
            public readonly IUserWithAccount User;
            public readonly int Count;

            public UserData(IUserWithAccount user, int count)
            {
                User = user ?? throw new ArgumentNullException(nameof(user));
                Count = count.TryThrowLessOrEqualZeroException();
            }
        }
    }
}