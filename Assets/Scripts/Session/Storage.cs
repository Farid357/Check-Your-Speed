using CheckYourSpeed.SaveSystem;
using System;
using IDisposable = CheckYourSpeed.Model.IDisposable;

namespace CheckYourSpeed.Logging
{
    public sealed partial class SessionsCounter
    {
        public sealed class Storage : IDisposable
        {
            private readonly SessionsCounter _counter;
            private readonly IStorage _storage;
            private const string Path = "Attempts";

            public Storage(SessionsCounter counter, IStorage storage)
            {
                _counter = counter ?? throw new ArgumentNullException(nameof(counter));
                _storage = storage ?? throw new ArgumentNullException(nameof(storage));
                _counter.SetCount(_storage.Load<int>(Path));
                _counter._onChangedUserData += Save;
            }

            private void Save(int count) => _storage.Save(Path, count);

            public void Dispose() => _counter._onChangedUserData -= Save;

        }
    }
}