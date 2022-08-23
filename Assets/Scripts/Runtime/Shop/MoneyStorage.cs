using System;
using CheckYourSpeed.SaveSystem;

namespace CheckYourSpeed.Shop
{
    public sealed class MoneyStorage
    {
        private readonly IStorage _storage;
        private const string Path = "Wallet";

        public MoneyStorage(IStorage storage)
        {
            _storage = storage ?? throw new ArgumentNullException(nameof(storage));
        }

        public bool HasSave() => _storage.Exists(Path);

        public int Load() => _storage.Load<int>(Path);

        public void Save(int money) => _storage.Save(Path, money);

    }
}