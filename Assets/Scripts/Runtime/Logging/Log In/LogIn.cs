using CheckYourSpeed.Utils;
using System.Collections.Generic;
using System.Linq;
using CheckYourSpeed.SaveSystem;
using UnityEngine;
using UnityEngine.UI;

namespace CheckYourSpeed.Loging
{
    public sealed class LogIn : MonoBehaviour
    {
        [SerializeField, RequireInterface(typeof(INotFoundUserView))] private MonoBehaviour _notFoundUserView;
        [SerializeField] private InputFields _inputFields;
        [SerializeField] private Button _registerButton;

        private System _system;

        public ISystem System => _system;

        public void Init(IStorage storage)
        {
            _system = new System(_inputFields.Name, _inputFields.Password, storage);
            _registerButton.onClick.AddListener(TryLogIn);
        }

        private void TryLogIn()
        {
            if (_inputFields.All.HasNotAny(field => field.TextInvalid) && _inputFields.All.All(field => field.TextNotEmpty))
            {
                if (ContainsSameUserData(_system.EnteredUsers))
                {
                    _system.InviteUser();
                }

                else
                {
                    _notFoundUserView.ToInterface<INotFoundUserView>().StartVisualize();
                }
            }
        }

        private bool ContainsSameUserData(IReadOnlyList<IUserWithAccount> users)
        {
            if (users.Count == 0)
                return false;

            return users.Any(user => user.Name == _inputFields.Name.Text && user.Password == _inputFields.Password.Text);
        }
    }
}