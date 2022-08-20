using CheckYourSpeed.Utils;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace CheckYourSpeed.Loging
{
    public sealed class Registration : MonoBehaviour
    {
        [SerializeField] private PasswordField _passwordField;
        [SerializeField] private NameField _nameField;
        [SerializeField] private Button _button;
        private System _system;

        public ISystem System => _system;

        public void Init(UsersStorage storage)
        {
            storage = storage ?? throw new ArgumentNullException(nameof(storage));
            _system = new System(_nameField, _passwordField, storage);
            _button.onClick.AddListener(TryRegister);
        }

        private void TryRegister()
        {
            if (_system.EnteredUsers.HasNotAny(user => user.Name.Equals(_nameField.Text)))
            {
                _system.CreateNewUser();
            }
        }

        private void OnDestroy() => _button.onClick.RemoveListener(TryRegister);

    }
}