using System;
using UnityEngine;
using UnityEngine.UI;

namespace CheckYourSpeed.App
{
    [RequireComponent(typeof(Button))]
    public abstract class AppStateButton : MonoBehaviour
    {
        private Button _button;

        public void Init(PauseBroadcaster pauseBroadcaster)
        {
            if (pauseBroadcaster is null)
            {
                throw new ArgumentNullException(nameof(pauseBroadcaster));
            }

            _button = GetComponent<Button>();
            _button.onClick.AddListener(() => OnClick(pauseBroadcaster));
        }

        protected abstract void OnClick(PauseBroadcaster pauseBroadcaster);
    }
}
