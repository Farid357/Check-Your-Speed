using DG.Tweening;
using TMPro;
using UnityEngine;

namespace CheckYourSpeed.GameLogic
{
    public class TextView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField] private float _duration = 1.5f;

        protected void Display(int count)
        {
            _text.DOText(count.ToString(), _duration, scrambleMode: ScrambleMode.Numerals);
        }
    }
}
