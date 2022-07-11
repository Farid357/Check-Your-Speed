using UnityEngine;

namespace CheckYourSpeed.Settings
{
    [CreateAssetMenu(fileName = "Difficulty Congig", menuName = "Create/Difficulty Config")]
    public sealed class DifficultyConfig : ScriptableObject
    {
        [SerializeField] private Difficulty _default;
        private Difficulty _difficulty;

        public void SetSelectedDifficulty(Difficulty difficulty)
        {
            _difficulty = difficulty ?? throw new System.ArgumentNullException(nameof(difficulty));
        }

        public Difficulty GetSelected()
        {
            return _difficulty == null ? _default : _difficulty;
        }
    }
}
