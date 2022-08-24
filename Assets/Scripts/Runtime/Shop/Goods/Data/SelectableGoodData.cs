using CheckYourSpeed.Shop;
using UnityEngine;

namespace CheckYourSpeed.Root
{
    [CreateAssetMenu(fileName = "Selectable Good Data", menuName = "Create/Selectable Good Data")]

    public sealed class SelectableGoodData : ScriptableObject
    {
        [field: SerializeField] public SelectableGoodVisualization Selectable { get; private set; }

        [field: SerializeField] public GoodData Data { get; private set; }

    }
}
