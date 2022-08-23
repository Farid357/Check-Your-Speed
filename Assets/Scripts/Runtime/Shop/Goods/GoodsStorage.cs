using System;
using System.Collections.Generic;
using CheckYourSpeed.SaveSystem;

namespace CheckYourSpeed.Shop
{
    public sealed class GoodsStorage
    {
        private readonly IStorage _storage;
        private const string Path = "BuyedGoods";

        public GoodsStorage(IStorage storage)
        {
            _storage = storage ?? throw new ArgumentNullException(nameof(storage));
        }

        public bool HasSave() => _storage.Exists(Path);

        public List<IGood> Load()
        {
            return _storage.Load<List<IGood>>(Path);
        }

        public void Save(List<IGood> goods)
        {
            if (goods is null)
            {
                throw new ArgumentNullException(nameof(goods));
            }

            _storage.Save(Path, goods);
        }
    }
}