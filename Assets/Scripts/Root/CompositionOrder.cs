using UnityEngine;

namespace CheckYourSpeed.Root
{
    public sealed class CompositionOrder : MonoBehaviour
    {
        [SerializeField] private CompositeRoot[] _roots;

        private void Awake()
        {
            foreach (var root in _roots)
            {
                root.Compose();
                root.enabled = true;
            }
        }
    }
}