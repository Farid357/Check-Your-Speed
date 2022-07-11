using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CheckYourSpeed.Loging
{
    public abstract class UserLogIn : MonoBehaviour, IUserFinder
    {
        [SerializeField] private NameField _nameField;
        [SerializeField] private PasswordField _passwordField;
        [SerializeField] private Button _logButton;

        private readonly List<LogInField> _fields = new();
        private List<User> _users = new();
        private Func<List<LogInField>, bool> _logingPredicate;
        private Func<List<User>, bool> _userPredicate;
        private UserLogInStorage _storage;

        public event Action OnNotFoundUser;
        public event Action<User> OnFoundUser;

        public void Init(Func<List<LogInField>, bool> predicate, Func<List<User>, bool> userPredicate, UserLogInStorage storage)
        {
            _logingPredicate = predicate ?? throw new ArgumentNullException(nameof(predicate));
            _userPredicate = userPredicate ?? throw new ArgumentNullException(nameof(userPredicate));
            _storage = storage ?? throw new ArgumentNullException(nameof(storage));
            _fields.AddRange(new List<LogInField> { _nameField, _passwordField });
            _users = _storage.Load();
            _logButton.onClick.AddListener(TryLogIn);
        }

        private void TryLogIn()
        {
            if (_logingPredicate.Invoke(_fields) && _userPredicate.Invoke(_users))
            {
                LogIn();
            }

            else
            {
                OnNotFoundUser?.Invoke();
            }
        }

        private void LogIn()
        {
            var user = new User(_nameField.Text, _passwordField.Text);
            _users.Add(user);
            _storage.Save(_users);
            OnFoundUser?.Invoke(user);
        }
    }
}