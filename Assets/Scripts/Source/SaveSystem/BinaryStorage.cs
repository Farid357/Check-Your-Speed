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
            var allPath = Path.Combine(Application.persistentDataPath, path);
            if (Exists(allPath))
            {
                File.Delete(allPath);
            }
        }

        public T Load<T>(string path)
        {
            var allPath = Path.Combine(Application.persistentDataPath, path);
            if (Exists(allPath))
            {
                using FileStream file = File.Open(allPath, FileMode.Open);
                return (T)_formatter.Deserialize(file);
            }

            return default;
        }

        public bool Exists(string path)
        {
            var allPath = Path.Combine(Application.persistentDataPath, path);
            return File.Exists(allPath);
        }

        public void Save<T>(string path, T saveObject)
        {
            var allPath = Path.Combine(Application.persistentDataPath, path);
            using var file = File.Create(allPath);
            _formatter.Serialize(file, saveObject);
        }
    }
}
