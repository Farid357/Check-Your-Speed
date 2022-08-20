using System;
using UnityEngine;
using UnityEngine.UI;

namespace CheckYourSpeed.App
{
    [RequireComponent(typeof(Button))]
    public abstract class AppStateButton : MonoBehaviour
    {
        private Button _button;

        public void Init(IPauseSwitch pauseSwitch)
        {
            if (pauseSwitch is null)
            {
                throw new ArgumentNullException(nameof(pauseSwitch));
            }

            _button = GetComponent<Button>();
            _button.onClick.AddListener(() => OnClick(pauseSwitch));
        }

        protected abstract void OnClick(IPauseSwitch pauseSwitch);
    }
}
