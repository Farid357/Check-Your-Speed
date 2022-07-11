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

        public event Action<List<User>> OnChangedData;
        public event Action OnNotFoundUser;
        public event Action<User> OnFoundUser;

        public void Init(Func<List<LogInField>, bool> predicate, Func<List<User>, bool> userPredicate)
        {
            _logingPredicate = predicate ?? throw new ArgumentNullException(nameof(predicate));
            _userPredicate = userPredicate ?? throw new ArgumentNullException(nameof(userPredicate));
            _fields.AddRange(new List<LogInField> { _nameField, _passwordField });
            _logButton.onClick.AddListener(TryLogIn);
        }

        public void Init(List<User> users)
        {
            _users = users ?? throw new ArgumentNullException(nameof(users));
        }

        private void TryLogIn()
        {
            if (_logingPredicate.Invoke(_fields) && _userPredicate.Invoke(_users))
            {
                LogIn();
            }

            else
            {
                OnNotFoundUser.Invoke();
            }
        }

        private void LogIn()
        {
            var user = new User(_nameField.Text, _passwordField.Text);
            _users.Add(user);
            OnChangedData?.Invoke(_users);
            OnFoundUser?.Invoke(user);
        }
    }
}