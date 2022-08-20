using CheckYourSpeed.SaveSystem;
using CheckYourSpeed.Utils;
using System;

namespace CheckYourSpeed.Loging
{
    public sealed class SessionsCounterStorage
    {
        private readonly IStorage _storage;
        private const string Path = "AttemptCount";

        public SessionsCounterStorage(IStorage storage)
        {
            _storage = storage ?? throw new ArgumentNullException(nameof(storage));
        }

        public int Load(IUserWithAccount user)
        {
            var data = _storage.Load<UserData>(Path + user.Password + user.Name);
            return data.Count;
        }

        public void Save(IUserWithAccount user, int count)
        {
            var data = new UserData(user, count);
            _storage.Save(Path + user.Password + user.Name, data);
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