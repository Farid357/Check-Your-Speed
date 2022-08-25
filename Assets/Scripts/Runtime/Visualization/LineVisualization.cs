using CheckYourSpeed.Model;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace CheckYourSpeed.GameLogic
{
    public  sealed  class  LineVisualization : MonoBehaviour, IVisualization<string>
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField] private float _duration = 1.5f;
        [SerializeField] private ScrambleMode _scrambleMode = ScrambleMode.Numerals;
        
        public void Visualize(string line)
        {
            _text.DOText(line, _duration, scrambleMode: _scrambleMode);
        }
    }
}