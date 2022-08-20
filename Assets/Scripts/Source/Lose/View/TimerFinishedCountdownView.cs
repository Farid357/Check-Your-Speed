using CheckYourSpeed.Model;
using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace CheckYourSpeed.GameLogic
{
    public sealed class TimerFinishedCountdownView : MonoBehaviour
    {
        [SerializeField] private LoseWindow _panel;
        [SerializeField] private float _delay = 0.9f;
        [SerializeField] private Color _loseColor = Color.red;
        [SerializeField] private Image _loseImage;

        private ITimer _timer;

        public void Init(ITimer timer)
        {
            _timer = timer ?? throw new ArgumentNullException(nameof(timer));
        }

        private void Update()
        {
            if (_timer.FinishedCountdown)
            {
                Lose();
            }
        }

        private void Lose()
        {
            _panel.Show();
            _loseImage.DOColor(_loseColor, _delay);
        }
    }
}