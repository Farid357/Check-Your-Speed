using System;

namespace CheckYourSpeed.SaveSystem
{
    public sealed class StorageWithNameSaveObject<TStorageUser, TStoreValue>
    {
        private readonly IStorage _storage;
        private readonly string _path;
        
        public StorageWithNameSaveObject(IStorage storage)
        {
            _storage = storage ?? throw new ArgumentNullException(nameof(storage));
            _path = typeof(TStoreValue).Name + typeof(TStorageUser).Name;
        }

        public StorageWithNameSaveObject() : this(new BinaryStorage())
        {
            
        }
        
        public bool HasSave() => _storage.Exists(_path);

        public TStoreValue Load() => _storage.Load<TStoreValue>(_path);

        public void Save(TStoreValue self) => _storage.Save(_path, self);
    }
}