using CheckYourSpeed.SaveSystem;
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

        public int Load()
        {
            var data = _storage.Load<UserData>(Path);
            return data.User != null ? data.Count : 0;
        }

        public void Save(IUser user, int count)
        {
            var data = new UserData(user, count);
            _storage.Save(Path, data);
        }

        [Serializable]
        private readonly struct UserData
        {
            public readonly IUser User;
            public readonly int Count;

            public UserData(IUser user, int count)
            {
                User = user ?? throw new ArgumentNullException(nameof(user));
                Count = count;
            }
        }
    }
}