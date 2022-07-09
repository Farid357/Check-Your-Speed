using DG.Tweening;
using System;
using TMPro;
using UnityEngine;

namespace CheckYourSpeed.Logging
{
    public sealed class SessionsCounterView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField] private float _duration;

        private SessionsCounter _sessionsCounter;

        public void Init(SessionsCounter sessionsCounter)
        {
            _sessionsCounter = sessionsCounter ?? throw new ArgumentNullException(nameof(sessionsCounter));
            _sessionsCounter.OnChanged += Display;
        }

        private void OnDisable() => _sessionsCounter.OnChanged -= Display;

        private void Display(int count)
        {
            _text.text = count.ToString();
            _text.DOText(count.ToString(), _duration, scrambleMode: ScrambleMode.Numerals);
        }
    }
}