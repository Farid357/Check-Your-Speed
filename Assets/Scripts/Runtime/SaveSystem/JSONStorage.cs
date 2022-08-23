using UnityEngine;
using System.IO;

namespace CheckYourSpeed.SaveSystem
{
    public sealed class JSONStorage : IStorage
    {
        public bool Exists(string path) => File.Exists(path);

        public T Load<T>(string path)
        {
            var jsonPath = string.Empty;

#if UNITY_ANDROID && !UNITY_EDITOR
            jsonPath = Path.Combine(Application.persistentDataPath + path);
#else
            jsonPath = Path.Combine(Application.dataPath + path);
#endif
            if (Exists(jsonPath))
            {
                var saveJson = File.ReadAllText(jsonPath);
                return JsonUtility.FromJson<T>(saveJson);
            }
            return default;
        }

        public void Save<T>(string path, T saveObject)
        {
            var jsonPath = string.Empty;
#if UNITY_ANDROID && !UNITY_EDITOR
            jsonPath = Path.Combine(Application.persistentDataPath + path);
#else
            jsonPath = Path.Combine(Application.dataPath + path);
#endif
            var saveJson = JsonUtility.ToJson(saveObject);
            File.WriteAllText(jsonPath, saveJson);
        }
    }
}