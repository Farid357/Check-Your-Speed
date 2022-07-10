using CheckYourSpeed.Logging;
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
        [SerializeField] private LoggIn _userLoggIn;
        [SerializeField] private Registration _registration;
        [SerializeField] private FakeLogging _nameLogging;
        [SerializeField] private UserConfig _config;
        [SerializeField] private Button _close;
        [SerializeField] private UserLogginView _logginView;

        private readonly List<IDisposable> _disposables = new();

        public override void Compose()
        {
            _close.onClick.AddListener(Close);
            _config.SetUser(new WithoutRegisteringUser());
            _userLoggIn.Init(loggings => loggings.HasNotAny(logging => logging.Invalid) && loggings.All(l => l.NotEmpty),
             users => users.Any(user => user.Name == _nameLogging.Text == true));
            var logginStorage = new LoggIn.UserLogginStorage(new BinaryStorage(), _userLoggIn);
            var sessionsCounter = new SessionsCounter(new LoseTimer(1), new WithoutRegisteringUser());
            var sessionStorage = new SessionsCounter.Storage(sessionsCounter, new BinaryStorage());
            _sessionsCounterView.Init(sessionsCounter);
            _registration.Init(loggin => true, user => true);
            var registrationStorage = new Registration.UserLogginStorage(new BinaryStorage(), _registration);
            _disposables.AddRange(new List<IDisposable> { sessionsCounter, sessionStorage, logginStorage, registrationStorage });
            _userLoggIn.OnFoundUser += ChangeUser;
            _registration.OnFoundUser += ChangeUser;
        }

        private void Close()
        {
            _config.SetUser(new WithoutRegisteringUser());
            _logginView.DisableUserUI();
        }

        private void ChangeUser(IUser user)
        {
            _config.SetUser(user);
            _logginView.DisableUserUI();
        }

        private void OnDisable()
        {
            _disposables.ForEach(disposable => disposable.Dispose());
            _userLoggIn.OnFoundUser -= ChangeUser;
            _registration.OnFoundUser -= ChangeUser;
        }

        private void OnDestroy() => _close.onClick.RemoveListener(Close);

    }
}