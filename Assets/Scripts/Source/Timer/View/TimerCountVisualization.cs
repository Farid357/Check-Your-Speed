using CheckYourSpeed.Model;
using UnityEngine;
using TMPro;
using System;

namespace CheckYourSpeed.GameLogic
{
    public sealed class TimerCountVisualization : MonoBehaviour, IVisualization<float>
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField] private Color _endTimeColor = Color.red;
        private Color _startTextColor;

        private void Awake() => _startTextColor = _text.color;
       
        public void Visualize(float time)
        {
            var text = Math.Round(time, 1).ToString();
            _text.text = text;
            _text.color = time < 1.5f ? _endTimeColor : _startTextColor;
        }
    }
}