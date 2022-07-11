using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace CheckYourSpeed.Settings
{
    public sealed class DifficultyDropdown : MonoBehaviour
    {
        [SerializeField] private TMP_Dropdown _dropdown;
        private Difficulties _difficulties;

        public void Init(Difficulties difficulties)
        {
            _difficulties = difficulties ?? throw new System.ArgumentNullException(nameof(difficulties));
            Create();
        }

        private void Create()
        {
            var options = new List<TMP_Dropdown.OptionData>();
            for (int i = 0; i < _difficulties.All.Count(); i++)
            {
                var difficulty = _difficulties.All.ElementAt(i);
                options.Add(new TMP_Dropdown.OptionData(difficulty.Name, difficulty.Sprite));
            }

            _dropdown.AddOptions(options);
        }
    }
}
