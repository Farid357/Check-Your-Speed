using CheckYourSpeed.Model;
using TMPro;
using UnityEngine;
using DG.Tweening;

namespace CheckYourSpeed.GameLogic
{
    public sealed class CountVisualizationWithPrefixes : MonoBehaviour, IVisualization<int>
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField] private float _seconds = 1.5f;
        [SerializeField] private ScrambleMode _scrambleMode = ScrambleMode.Numerals;
        private readonly DigitsFormatter _digitsFormatter = new();

        public void Visualize(int count)
        {
            var text = _digitsFormatter.TryFormat(count);
            _text.DOText(text, _seconds, scrambleMode: _scrambleMode);
        }
    }
}