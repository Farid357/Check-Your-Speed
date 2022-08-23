using TMPro;
using UnityEngine;
using CheckYourSpeed.Utils;

namespace CheckYourSpeed.Loging
{
    public abstract class InputField : MonoBehaviour, IInputField
    {
        [SerializeField] private TMP_InputField _inputField;
        [SerializeField] private InvalidSymbolsContainer _container;
        [SerializeField, RequireInterface(typeof(IHelpBox))] private MonoBehaviour _helpBox;

        private IHelpBox HelpBox => _helpBox as IHelpBox;

        public string Text => _inputField.text;

        public bool TextInvalid { get; private set; } = true;

        public bool TextNotEmpty => !string.IsNullOrWhiteSpace(Text);

        private void Awake()
        {
            _inputField.onEndEdit.AddListener(Validate);
            HelpBox.VisualizeError();
        }

        private void Validate(string text)
        {
            TextInvalid = _container.IsInvalidText(text);

            if (TextInvalid || string.IsNullOrWhiteSpace(Text))
            {
                HelpBox.VisualizeError();
            }

            else
            {
                HelpBox.VisualizeCorrect();
            }
        }
    }

    public interface IInputField
    {
        public bool TextInvalid { get; }

        public bool TextNotEmpty { get; }
    }
}