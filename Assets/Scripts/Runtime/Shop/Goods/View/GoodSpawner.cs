using CheckYourSpeed.Shop.Visualization;
using UnityEngine;

namespace CheckYourSpeed.Shop.Data
{
    public sealed class GoodSpawner : MonoBehaviour
    {
        [SerializeField] private Transform _parent;

        public GoodVisualization Spawn(GoodVisualization prefab)
        {
            return Instantiate(prefab, _parent);
        }
    }
}
