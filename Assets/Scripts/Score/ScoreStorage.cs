using CheckYourSpeed.Model;
using UnityEngine;

namespace CheckYourSpeed.GameLogic
{
    public sealed class ScoreStorage : IScoreStorage
    {
        private const string Path = "ScoreRecord";

        public Score Load()
        {
            var loadJson = PlayerPrefs.GetString(Path);
            return JsonUtility.FromJson<Score>(loadJson);
        }

        public void Save(Score score)
        {
            if (score is null)
            {
                throw new System.ArgumentNullException(nameof(score));
            }

            var json = JsonUtility.ToJson(score);
            PlayerPrefs.SetString(Path, json);
            PlayerPrefs.Save();
        }
    }
}
