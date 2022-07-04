using CheckYourSpeed.Model;
using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace CheckYourSpeed.GameLogic
{
    public sealed class LoseTimerView : MonoBehaviour
    {
        [SerializeField] private LosePanel _panel;
        [SerializeField] private float _delay = 0.9f;
        [SerializeField] private Color _loseColor = Color.red;
        [SerializeField] private Image _loseImage;

        private LoseTimer _loseTimer;

        public void Init(LoseTimer loseTimer)
        {
            _loseTimer = loseTimer ?? throw new ArgumentNullException(nameof(loseTimer));
            _loseTimer.OnEnded += Lose;
        }

        private void OnDisable() => _loseTimer.OnEnded -= Lose;

        private void Lose()
        {
            _panel.Show();
            _loseImage.DOColor(_loseColor, _delay);
        }
    }
}