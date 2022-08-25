using System;

namespace CheckYourSpeed.SaveSystem
{
    public sealed class StorageWithNameSaveObject<T>
    {
        private readonly IStorage _storage;
        private readonly string _path;
        
        public StorageWithNameSaveObject(IStorage storage)
        {
            _storage = storage ?? throw new ArgumentNullException(nameof(storage));
            _path = nameof(T);
        }
        
        public bool HasSave() => _storage.Exists(_path);

        public T Load() => _storage.Load<T>(_path);

        public void Save(T self) => _storage.Save(_path, self);
    }
}