using CheckYourSpeed.SaveSystem;
using CheckYourSpeed.Utils;

namespace CheckYourSpeed.Loging
{
    public sealed class SessionCounterStorage : IUserCounterStorage
    {
        private readonly IUserCounterStorage _counterStorage;
        private const string Path = "SessingCountStorage";

        public SessionCounterStorage(IUserWithAccount user)
        {
            _counterStorage = new UserCounterStorage(new BinaryStorage(), user, Path);
        }

        public int Load() => _counterStorage.Load();

        public void Save(int count)
        {
            count.TryThrowLessThanOrEqualsToZeroException();
            _counterStorage.Save(count);
        }
    }
}