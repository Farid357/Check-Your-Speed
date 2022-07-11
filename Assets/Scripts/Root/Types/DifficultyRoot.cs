using CheckYourSpeed.Settings;
using UnityEngine;

namespace CheckYourSpeed.Root
{
    public sealed class DifficultyRoot : CompositeRoot
    {
        [SerializeField] private Difficulties _difficulties;
        [SerializeField] private DifficultySelector _selector;
        [SerializeField] private DifficultyDropdown _dropdown;

        public override void Compose()
        {
            _dropdown.Init(_difficulties);
            _selector.Init(_difficulties);
        }
    }
}
