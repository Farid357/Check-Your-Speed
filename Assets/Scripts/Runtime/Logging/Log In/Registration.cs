using CheckYourSpeed.Utils;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace CheckYourSpeed.Loging
{
    public sealed class Registration : MonoBehaviour
    {
        [SerializeField] private InputFields _inputFields;
        [SerializeField, RequireInterface(typeof(INotFoundUserView))] private MonoBehaviour _notFoundUserView;
        [SerializeField] private Button _button;
        private System _system;

        public ISystem System => _system;

        public void Init(UsersStorage storage)
        {
            storage = storage ?? throw new ArgumentNullException(nameof(storage));
            _system = new System(_inputFields.Name, _inputFields.Password, storage);
            _button.onClick.AddListener(TryRegister);
        }

        private void TryRegister()
        {
            if (_system.EnteredUsers.HasNotAny(user => user.Name.Equals(_inputFields.Name.Text)))
            {
                _system.CreateNewUser();
            }

            else
            {
                _notFoundUserView.ToInterface<INotFoundUserView>().StartVisualize();
            }
        }

        private void OnDestroy() => _button.onClick.RemoveListener(TryRegister);

    }
}