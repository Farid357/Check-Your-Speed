using UnityEngine;

namespace CheckYourSpeed.Settings
{
    [CreateAssetMenu(fileName = "Difficulty", menuName = "Create/Difficulty")]
    public sealed class Difficulty : ScriptableObject
    {
        [field: SerializeField] public float CatchTime { get; private set; } = 1.5f;
        [field: SerializeField] public string Name { get; private set; } = "Легкий";
        [field: SerializeField] public Sprite Sprite { get; private set; }
    }
}
