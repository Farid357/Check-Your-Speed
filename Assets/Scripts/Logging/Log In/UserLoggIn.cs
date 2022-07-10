using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CheckYourSpeed.Logging
{
    public abstract partial class UserLoggIn : MonoBehaviour
    {
        [SerializeField] private NameLogging _nameLogging;
        [SerializeField] private PassWordLogging _passwordLogging;
        [SerializeField] private Button _logButton;

        private readonly List<FakeLogging> _loggings = new();
        private User _lastUser;
        private List<User> _users = new();
        private Func<List<FakeLogging>, bool> _loggingPredicate;
        private Func<List<User>, bool> _userPredicate;

        public event Action<User> OnChangedUser;
        public event Action<User> OnFoundUser;
        private event Action<User, List<User>> _onChangedData;

        private void Start()
        {
            if (_lastUser != null)
            {
                OnFoundUser?.Invoke(_lastUser);
            }
        }

        public void Init(Func<List<FakeLogging>, bool> predicate, Func<List<User>, bool> userPredicate)
        {
            _loggingPredicate = predicate ?? throw new ArgumentNullException(nameof(predicate));
            _userPredicate = userPredicate ?? throw new ArgumentNullException(nameof(userPredicate));
            _loggings.AddRange(new List<FakeLogging> { _nameLogging, _passwordLogging });
            _logButton.onClick.AddListener(TryLogIn);
        }

        private void TryLogIn()
        {
            if (_loggingPredicate.Invoke(_loggings))
            {
                if (_userPredicate.Invoke(_users))
                {
                    Debug.Log("log");
                    LogIn();
                }
            }
        }

        private void LogIn()
        {
            var user = new User(_nameLogging.Text, _passwordLogging.Text);
            _users.Add(user);
            _onChangedData?.Invoke(_lastUser, _users);
            OnChangedUser?.Invoke(user);
        }
    }
}