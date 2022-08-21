using UnityEngine;
using System.IO;

namespace CheckYourSpeed.SaveSystem
{
    public sealed class JSONStorage : IStorage
    {
        public bool Exists(string path) => File.Exists(path);

        public T Load<T>(string path) 
        {
            var jsonPath = Path.Combine(Application.persistentDataPath + path);

            if (Exists(jsonPath))
            {
                var saveJson = File.ReadAllText(jsonPath);
                return JsonUtility.FromJson<T>(saveJson);
            }
            return default;
        }

        public void Save<T>(string path, T saveObject)
        {
            var saveJson = JsonUtility.ToJson(saveObject);
            var jsonPath = Path.Combine(Application.persistentDataPath + path);
            File.WriteAllText(jsonPath, saveJson);
        }
    }
}