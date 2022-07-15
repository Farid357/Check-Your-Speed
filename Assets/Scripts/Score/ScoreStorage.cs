using CheckYourSpeed.Utils;
using UnityEngine;

namespace CheckYourSpeed.Model
{
    public sealed class ScoreStorage : IScoreStorage
    {
        private const string Path = "ScoreRecord";

        public Score Load()
        {
            var loadJson = PlayerPrefs.GetString(Path);
            return loadJson.FromJson<Score>();
        }

        public void Save(Score score)
        {
            if (score is null)
            {
                throw new System.ArgumentNullException(nameof(score));
            }

            var json = score.ToJson();
            PlayerPrefs.SetString(Path, json);
            PlayerPrefs.Save();
        }
    }
}
