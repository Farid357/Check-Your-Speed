using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace CheckYourSpeed.SaveSystem
{
    public sealed class BinaryStorage : IStorage, IDeletable
    {
        private readonly BinaryFormatter _formatter = new();

        public void Delete(string path)
        {
            var allPath = CreatePath(path);
            if (Exists(allPath))
            {
                File.Delete(allPath);
            }
        }

        public T Load<T>(string path)
        {
            var allPath = CreatePath(path);
            if (Exists(path))
            {
                using FileStream file = File.Open(allPath, FileMode.Open);
                return (T)_formatter.Deserialize(file);
            }

            return default;
        }

        public bool Exists(string name) => File.Exists(CreatePath(name));

        public void Save<T>(string path, T saveObject)
        {
            var allPath = CreatePath(path);
            using var file = File.Create(allPath);
            _formatter.Serialize(file, saveObject);
        }

        private string CreatePath(string name)
        {
            return Path.Combine(Application.persistentDataPath, name);
        }
    }
}
