using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

namespace CheckYourSpeed.Logging
{
    [CreateAssetMenu(fileName = "Symbols", menuName = "Create/Invalid Symbols")]
    public sealed class InvalidSymbolsContainer : ScriptableObject
    {
        [SerializeField] private List<string> _symbols;

        public bool IsValidText(string text)
        {
            var matchCount = 0;
            List<MatchCollection> matches = GetMatches(text);

            foreach (var collection in matches)
            {
                matchCount += matches.Count;
            }

            return matchCount == 0;
        }

        private List<MatchCollection> GetMatches(string text)
        {
            var regices = new List<Regex>();
            var matches = new List<MatchCollection>();

            foreach (var symbol in _symbols)
            {
                var regex = new Regex(symbol);
                regices.Add(regex);
                matches.Add(regex.Matches(text));
            }

            return matches;
        }
    }
}