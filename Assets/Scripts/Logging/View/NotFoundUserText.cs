using Cysharp.Threading.Tasks;
using System;
using TMPro;
using UnityEngine;

namespace CheckYourSpeed.Loging
{
    [RequireComponent(typeof(TMP_Text))]
    public sealed class NotFoundUserText : MonoBehaviour
    {
        [SerializeField] private UserLogIn _userLogIn;
        [SerializeField] private float _showDelay = 1.8f;
        private TMP_Text _text;

        public void Enable()
        {
            _text = GetComponent<TMP_Text>();
            _userLogIn.OnNotFoundUser += Display;
        }

        private void OnDestroy() => _userLogIn.OnNotFoundUser -= Display;

        private void Display() => DisplayText();

        private async UniTaskVoid DisplayText()
        {
            _text.gameObject.SetActive(true);
            await UniTask.Delay(TimeSpan.FromSeconds(_showDelay));
            _text.gameObject.SetActive(false);
        }
    }
}