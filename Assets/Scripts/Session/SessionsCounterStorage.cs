using CheckYourSpeed.SaveSystem;
using System;
using IDisposable = CheckYourSpeed.Model.IDisposable;

namespace CheckYourSpeed.Loging
{
    public sealed class SessionsCounterStorage : IDisposable
    {
        private readonly SessionsCounter _counter;
        private readonly IStorage _storage;
        private const string Path = "Attempts";

        public SessionsCounterStorage(SessionsCounter counter, IStorage storage)
        {
            _counter = counter ?? throw new ArgumentNullException(nameof(counter));
            _storage = storage ?? throw new ArgumentNullException(nameof(storage));
            var data = _storage.Load<Data>(Path);

            if (data.User != null)
                _counter.SetCount(data.Count);
            _counter.OnChangedUserData += Save;
        }

        private void Save(User user, int count)
        {
            var data = new Data (user, count);
            _storage.Save(Path, data);
        }

        public void Dispose() => _counter.OnChangedUserData -= Save;

        private class Data
        {
            public readonly User User;
            public readonly int Count;

            public Data(User user, int count)
            {
                User = user ?? throw new ArgumentNullException(nameof(user));
                Count = count;
            }
        }
    }
}