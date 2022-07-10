using System;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

namespace CheckYourSpeed.Logging
{
    public abstract class FakeLogging : MonoBehaviour, ILogging
    {
        [SerializeField] private TMP_InputField _inputField;
        [SerializeField] private InvalidSymbolsContainer _container;

        public string Text { get; private set; }
        public bool Invalid { get; private set; } = true;

        public event Action OnFoundInvalidSymbols;
        public event Action OnLogged;

        private void Awake() => _inputField.onEndEdit.AddListener(Validate);

        private void Validate(string text)
        {
            Text = text;

            var regex = new Regex("", RegexOptions.IgnoreCase);
            var matches = regex.Matches(text);

            Invalid = _container.IsInvalidText(text);

            if (Invalid)
            {
                OnFoundInvalidSymbols?.Invoke();
            }

            else
            {
                OnLogged?.Invoke();
            }
        }
    }
}