using CheckYourSpeed.Loging;
using CheckYourSpeed.Model;
using CheckYourSpeed.SaveSystem;
using CheckYourSpeed.Utils;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using IDisposable = CheckYourSpeed.Model.IDisposable;

namespace CheckYourSpeed.Root
{
    public sealed class UserRoot : CompositeRoot
    {
        [SerializeField] private SessionsCounterView _sessionsCounterView;
        [SerializeField] private LogIn _userLogIn;
        [SerializeField] private Registration _registration;
        [SerializeField] private NameField _logInNameField;
        [SerializeField] private UserConfig _config;
        [SerializeField] private Button _close;
        [SerializeField] private UserLogInView _loggInView;
        [SerializeField] private UserLogInView _registrationView;
        [SerializeField] private PasswordField _passwordField;

        private readonly List<IDisposable> _disposables = new();

        public override void Compose()
        {
            _close.onClick.AddListener(Close);
            _config.SetUser(new WithoutRegisteringUser());
            _userLogIn.Init(logings => logings.HasNotAny(loging => loging.Invalid) && logings.All(l => l.NotEmpty),
             users => CheckUsers(users));
            var loginStorage = new UserLogInStorage(new BinaryStorage(), _userLogIn);
            var sessionsCounter = new SessionsCounter(new LoseTimer(1), new WithoutRegisteringUser());
            var sessionStorage = new SessionsCounterStorage(sessionsCounter, new BinaryStorage());
            _loggInView.Init(_userLogIn);
            _sessionsCounterView.Init(sessionsCounter);
            _registration.Init(loggin => true, user => true);
            var registrationStorage = new UserLogInStorage(new BinaryStorage(), _registration);
            _registrationView.Init(_registration);
            _disposables.AddRange(new List<IDisposable> { sessionsCounter, sessionStorage, loginStorage, registrationStorage });
            _userLogIn.OnFoundUser += ChangeUser;
            _registration.OnFoundUser += ChangeUser;
        }

        private bool CheckUsers(List<User> users)
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
            _config.SetUser(user);
            _loggInView.DisableUserUI();
        }

        private void OnDisable()
        {
            _disposables.ForEach(disposable => disposable.Dispose());
            _userLogIn.OnFoundUser -= ChangeUser;
            _registration.OnFoundUser -= ChangeUser;
        }

        private void OnDestroy() => _close.onClick.RemoveListener(Close);

    }
}