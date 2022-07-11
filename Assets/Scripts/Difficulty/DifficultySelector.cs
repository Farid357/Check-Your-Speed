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

        public void Init(Difficulties difficulties)
        {
            var difficultyName = _config.GetSelected().Name;

            if (_dropdown.captionText.text != difficultyName)
            {
                _dropdown.captionText.text = difficultyName;
            }

            _difficulties = difficulties ?? throw new System.ArgumentNullException(nameof(difficulties));
            _dropdown.onValueChanged.AddListener(Select);
        }

        private void OnDestroy() => _dropdown.onValueChanged.RemoveListener(Select);

        private void Select(int index)
        {
            var difficulty = _difficulties.All.ElementAt(index);
            _config.SetSelectedDifficulty(difficulty);
        }
    }
}
