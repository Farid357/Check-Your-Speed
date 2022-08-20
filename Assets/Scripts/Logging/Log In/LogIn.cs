using CheckYourSpeed.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace CheckYourSpeed.Loging
{
    public sealed class LogIn : MonoBehaviour
    {
        [SerializeField] private Button _registerButton;
        [SerializeField] private PasswordField _passwordField;
        [SerializeField] private NameField _nameField;
        private readonly List<InputField> _fields = new();
        private System _system;

        public ISystem System => _system;

        public void Init(UsersStorage storage)
        {
            storage = storage ?? throw new ArgumentNullException(nameof(storage));
            _system = new System(_nameField, _passwordField, storage);
            _fields.AddRange(_passwordField, _nameField);
            var users = storage.Load();
            _registerButton.onClick.AddListener(() => TryLogIn(users, _system));
        }

        private void TryLogIn(List<User> users, ISystem system)
        {
            if (_fields.HasNotAny(field => field.TextInvalid) && _fields.All(field => field.TextNotEmpty))
            {
                if (NotContainsSameUserData(users))
                {
                    system.LogInUser();
                }
            }
        }

        private bool NotContainsSameUserData(List<User> users)
        {
            if (users == null)
                return false;

            return users.Any(user => user.Name == _nameField.Text)
                && users.Any(user => user.Password == _passwordField.Text);
        }
    }
}