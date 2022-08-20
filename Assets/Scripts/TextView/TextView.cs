using DG.Tweening;
using TMPro;
using UnityEngine;
using CheckYourSpeed.Model;

namespace CheckYourSpeed.GameLogic
{
    public sealed class TextView : MonoBehaviour, ITextView
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField] private float _duration = 1.5f;
        [SerializeField] private ScrambleMode _scrambleMode = ScrambleMode.Numerals;

        public void Visualize(int count)
        {
            _text.DOText(count.ToString(), _duration, scrambleMode: _scrambleMode);
        }
    }
}
