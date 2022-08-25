using UnityEngine;
using System.IO;

namespace CheckYourSpeed.SaveSystem
{
    public sealed class JSONStorage : IStorage
    {
        public bool Exists(string path) => File.Exists(path);

        public T Load<T>(string name)
        {
            var jsonPath = CreatePath(name);

            if (Exists(jsonPath))
            {
                var saveJson = File.ReadAllText(jsonPath);
                return JsonUtility.FromJson<T>(saveJson);
            }
            return default;
        }

        public void Save<T>(string name, T saveObject)
        {
            var jsonPath = CreatePath(name);
            var saveJson = JsonUtility.ToJson(saveObject);
            File.WriteAllText(jsonPath, saveJson);
        }

        private string CreatePath(string name)
        {
            return Application.persistentDataPath + name;
        }
    }
}