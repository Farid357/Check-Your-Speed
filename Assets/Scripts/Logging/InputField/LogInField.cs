using System;
using TMPro;
using UnityEngine;

namespace CheckYourSpeed.Loging
{
    public abstract class LogInField : MonoBehaviour, ILoging
    {
        [SerializeField] private TMP_InputField _inputField;
        [SerializeField] private InvalidSymbolsContainer _container;

        public string Text => _inputField.text;

        public bool Invalid { get; private set; } = true;

        public bool NotEmpty => !string.IsNullOrWhiteSpace(Text);

        public event Action OnFoundInvalidSymbols;
        public event Action OnWrote;

        private void Awake()
        {
            _inputField.onEndEdit.AddListener(Validate);
            OnFoundInvalidSymbols?.Invoke();
        }

        private void Validate(string text)
        {
            Invalid = _container.IsInvalidText(text);

            if (Invalid || string.IsNullOrWhiteSpace(Text))
            {
                OnFoundInvalidSymbols?.Invoke();
            }

            else
            {
                OnWrote?.Invoke();
            }
        }
    }
}