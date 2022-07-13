using UnityEngine;

namespace CheckYourSpeed.SaveSystem
{
    public sealed class PlayerPrefsStorage : IStorage
    {
        public bool Exists(string key) => PlayerPrefs.HasKey(key);

        public T Load<T>(string key)
        {
            if (Exists(key))
            {
                var loadJson = PlayerPrefs.GetString(key);
                return JsonUtility.FromJson<T>(loadJson);
            }
            return default;
        }

        public void Save<T>(string key, T saveObject)
        {
            var saveJson = JsonUtility.ToJson(saveObject);
            PlayerPrefs.SetString(key, saveJson);
            PlayerPrefs.Save();
        }
    }
}
