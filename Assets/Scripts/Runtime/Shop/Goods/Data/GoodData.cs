using CheckYourSpeed.Shop;
using UnityEngine;

namespace CheckYourSpeed.Root
{
    [CreateAssetMenu(fileName = "Good Data", menuName = "Create/Good Data")]
    public sealed class GoodData : ScriptableObject
    {
        [field: SerializeField] public GoodVisualization Visualization { get; private set; }

        [field: SerializeField, Min(1)] public int Price { get; private set; }

        [field: SerializeField] public string Name { get; private set; }

        [field: SerializeField] public GoodIsBuyedVisualization IsBuyedVisualization { get; private set; }

        private void OnValidate()
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                Debug.LogError("Name is null or white space!");
            }

            if (Visualization is null)
            {
                Debug.LogError("Visualization is null!");
            }

            if (IsBuyedVisualization.name != Visualization.name)
            {
                Debug.LogError("IsBuyedVisualization and Visualization have to be on same GameObject!");
            }
        }
    }
}
