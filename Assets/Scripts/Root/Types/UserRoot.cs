using CheckYourSpeed.GameLogic;
using CheckYourSpeed.Loging;
using CheckYourSpeed.Model;
using CheckYourSpeed.SaveSystem;
using CheckYourSpeed.Utils;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace CheckYourSpeed.Root
{
    public sealed class UserRoot : CompositeRoot
    {
        [SerializeField] private TextView _textView;
        [SerializeField] private LogIn _userLogIn;
        [SerializeField] private Registration _registration;
        [SerializeField] private NameField _logInNameField;
        [SerializeField] private UserConfig _config;
        [SerializeField] private Button _close;
        [SerializeField] private UserLogInView _loggInView;
        [SerializeField] private UserLogInView _registrationView;
        [SerializeField] private PasswordField _passwordField;
        [SerializeField] private NotFoundUserText[] _notFoundUserTexts;
        [SerializeField] private NameField _registrationField;
        private SessionsCounter _sessionsCounter;

        public override void Compose()
        {
            _notFoundUserTexts.ToList().ForEach(text => text.Enable());
            _close.onClick.AddListener(Close);
            _config.SetUser(new WithoutRegisteringUser());
            var loginStorage = new UserLogInStorage(new BinaryStorage());
            _userLogIn.Init(logings => logings.HasNotAny(loging => loging.Invalid) && logings.All(l => l.NotEmpty),
             users => HaveAnyCorrectUserFrom(users), loginStorage);
            _loggInView.Init(_userLogIn);
            var registrationStorage = new UserLogInStorage(new BinaryStorage());
            _registration.Init(loggins => true, users => users.HasNotAny(user => user.Name.Equals(_registrationField.Text)), registrationStorage);
            _registrationView.Init(_registration);
            _userLogIn.OnFoundUser += ChangeUser;
            _registration.OnFoundUser += ChangeUser;
        }

        private bool HaveAnyCorrectUserFrom(List<User> users)
        {
            if (users == null)
                return false;

            return users.Any(user => user.Name == _logInNameField.Text) && users.Any(user => user.Password == _passwordField.Text);
        }

        private void Close()
        {
            _config.SetUser(new WithoutRegisteringUser());
            _loggInView.DisableUserUI();
        }

        private void ChangeUser(IUser user)
        {
            var sessionStorage = new SessionsCounterStorage(new BinaryStorage());
            _sessionsCounter = new SessionsCounter(new FakeTimer(), user, sessionStorage, _textView);
            _config.SetUser(user);
            _loggInView.DisableUserUI();
            _textView.Visualize(_sessionsCounter.Count);
        }

        private void OnDisable()
        {
            _userLogIn.OnFoundUser -= ChangeUser;
            _registration.OnFoundUser -= ChangeUser;
        }

        private void OnDestroy() => _close.onClick.RemoveListener(Close);

    }
}