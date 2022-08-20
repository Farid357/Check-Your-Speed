using CheckYourSpeed.Model;
using CheckYourSpeed.SaveSystem;
using CheckYourSpeed.Utils;

namespace CheckYourSpeed.GameLogic
{
    public sealed class ScoreRecordStorage : IScoreRecordStorage
    {
        private readonly IStorage _storage;
        private const string Key = "StorageRecord";

        public ScoreRecordStorage(IStorage storage)
        {
            _storage = storage ?? throw new System.ArgumentNullException(nameof(storage));
        }

        public int Load()
        {
            return _storage.Exists(Key) ? _storage.Load<int>(Key) : 0;
        }

        public void Save(int count)
        {
            count.TryThrowLessOrEqualZeroException();
            _storage.Save(Key, count);
        }
    }
}
