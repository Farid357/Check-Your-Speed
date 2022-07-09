using CheckYourSpeed.Model;
using CheckYourSpeed.SaveSystem;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace CheckYourSpeed.Logging
{
    public sealed class UserLogging : MonoBehaviour
    {
        [SerializeField] private FakeLogging _nameLogging;
        [SerializeField] private Button _logButton;
        [SerializeField] private FakeLogging _passwordLogging;
        [SerializeField] private HelpBox _nameHelpBox;

        private readonly List<FakeLogging> _loggings = new();
        private List<User> _users = new();
        private readonly IStorage _storage = new BinaryStorage();
        private const string Path = "Users";

        private void Awake()
        {
            _loggings.Add(_nameLogging);
            _loggings.Add(_passwordLogging);
            _logButton.onClick.AddListener(TryLogIn);
            _users = _storage.Load<List<User>>(Path);
        }

        private void TryLogIn()
        {
            if (_loggings.All(logging => logging.Invalid == false))
            {
                if (_users.Any(user => user.Name == _nameLogging.Text) == false)
                {
                    var user = new User(_nameLogging.Text, _passwordLogging.Text);
                    _users.Add(user);
                    _storage.Save(Path, _users);
                }
            }
        }
    }
}