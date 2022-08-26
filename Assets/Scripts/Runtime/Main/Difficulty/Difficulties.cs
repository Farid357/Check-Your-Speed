using System.Collections.Generic;
using UnityEngine;

namespace CheckYourSpeed.Settings
{
    [CreateAssetMenu(fileName = "Difficulties", menuName = "Create/Difficulties")]
    public sealed class Difficulties : ScriptableObject
    {
        [SerializeField] private List<Difficulty> _all;

        public IEnumerable<Difficulty> All => _all;
    }
}
