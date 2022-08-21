using CheckYourSpeed.Loging;
using CheckYourSpeed.Model;
using CheckYourSpeed.SaveSystem;
using CheckYourSpeed.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace CheckYourSpeed.Root
{
    public sealed class UserRoot : CompositeRoot
    {
        [SerializeField, RequireInterface(typeof(ITextView))] private MonoBehaviour _textView;
        [SerializeField] private LogIn _logIn;
        [SerializeField] private Registration _registration;
        [SerializeField] private UserConfig _config;
        [SerializeField] private Button _close;
        [SerializeField] private UserLogInView _loggInView;
        [SerializeField] private UserLogInView _registrationView;
        private SessionsCounter _sessionsCounter;

        public override void Compose()
        {
            _close.onClick.AddListener(Close);
            _config.SaveUser(new WithoutAccountUser());
            var storage = new UsersStorage(new BinaryStorage());
            _logIn.Init(storage);
            _loggInView.Init(_logIn.System);
            _registration.Init(storage);
            _registrationView.Init(_registration.System);
            _logIn.System.OnUserEntered += SwicthUser;
            _registration.System.OnUserEntered += SwicthUser;
        }

        private void Close()
        {
            _config.SaveUser(new WithoutAccountUser());
            _loggInView.ShowMenu();
        }

        private void SwicthUser(IUserWithAccount user)
        {
            var sessionStorage = new SessionCounterStorage(user);
            _sessionsCounter = new SessionsCounter(new DummyTimer(), sessionStorage, _textView.ToInterface<ITextView>());
            _config.SaveUser(user);
            _loggInView.ShowMenu();
            var textView = _textView as ITextView;
            textView.Visualize(_sessionsCounter.Count);
        }

        private void OnDisable()
        {
            _logIn.System.OnUserEntered -= SwicthUser;
            _registration.System.OnUserEntered -= SwicthUser;
        }

        private void OnDestroy() => _close.onClick.RemoveListener(Close);

    }
}