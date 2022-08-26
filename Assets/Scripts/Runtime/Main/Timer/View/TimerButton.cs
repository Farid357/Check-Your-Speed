using System;
using CheckYourSpeed.Model;
using UnityEngine;
using UnityEngine.UI;

namespace CheckYourSpeed.GameLogic
{
    [RequireComponent(typeof(Button))]
    public sealed class TimerButton : MonoBehaviour
    {
        private IReadOnlyTimer _timer;
        private Button _button;

        public void Init(IReadOnlyTimer timer)
        {
            _button = GetComponent<Button>();
            _button.interactable = false;
            _timer = timer ?? throw new ArgumentNullException(nameof(timer));
        }

        private void Update()
        {
            if (_timer.FinishedCountdown)
            {
                _button.interactable = true;
            }
        }
    }
}