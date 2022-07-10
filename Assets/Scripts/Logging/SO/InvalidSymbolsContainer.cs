using System.Collections.Generic;
using UnityEngine;

namespace CheckYourSpeed.Logging
{
    [CreateAssetMenu(fileName = "Symbols", menuName = "Create/Invalid Symbols")]
    public sealed class InvalidSymbolsContainer : ScriptableObject
    {
        [SerializeField] private List<string> _symbols;

        public bool IsInvalidText(string text)
        {
            int matches = GetMatchesCount(text);
            return matches > 0;
        }

        private int GetMatchesCount(string text)
        {
            var count = 0;

            foreach (var symbol in _symbols)
            {
                if(text.Contains(symbol))
                {
                    count++;
                }
            }

            return count;
        }
    }
}