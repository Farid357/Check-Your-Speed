using CheckYourSpeed.SaveSystem;
using System.Linq;
using TMPro;
using UnityEngine;

namespace CheckYourSpeed.Settings
{
    public sealed class DifficultySelector : MonoBehaviour
    {
        [SerializeField] private TMP_Dropdown _dropdown;
        [SerializeField] private DifficultyConfig _config;
        private Difficulties _difficulties;
        private IStorage _storage;

        private const string Path = "SelectedDifficulty";

        public void Init(Difficulties difficulties, IStorage storage)
        {
            _difficulties = difficulties ?? throw new System.ArgumentNullException(nameof(difficulties));
            _storage = storage ?? throw new System.ArgumentNullException(nameof(storage));
            _dropdown.value = _storage.Load<int>(Path);
            _dropdown.onValueChanged.AddListener(Select);
        }

        private void OnDestroy() => _dropdown.onValueChanged.RemoveListener(Select);

        private void Select(int index)
        {
            var difficulty = _difficulties.All.ElementAt(index);
            _config.SetSelectedDifficulty(difficulty);
            _storage.Save(Path, index);
        }
    }
}
