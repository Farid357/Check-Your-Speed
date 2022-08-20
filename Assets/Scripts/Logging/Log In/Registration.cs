using CheckYourSpeed.Utils;
using System;
using System.Collections.Generic;
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
            var users = storage.Load();
            _button.onClick.AddListener(() => TryRegister(users, _system));
        }

        private void TryRegister(List<User> users, ISystem system)
        {
            if (users.HasNotAny(user => user.Name.Equals(_nameField.Text)))
            {
                system.LogInNewUser();
            }
        }
    }
}