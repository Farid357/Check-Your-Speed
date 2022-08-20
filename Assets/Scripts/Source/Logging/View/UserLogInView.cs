using System;
using UnityEngine;
using CheckYourSpeed.Utils;

namespace CheckYourSpeed.Loging
{
    public sealed class UserLogInView : MonoBehaviour
    {
        [SerializeField] private Canvas _userCanvas;
        [SerializeField] private Canvas _menuCanvas;
        [SerializeField, RequireInterface(typeof(IUserEnterView))] private MonoBehaviour _userView;
        private ISystem _system;

        public void Init(ISystem system)
        {
            _system = system ?? throw new ArgumentNullException(nameof(system));
            _system.OnUserEntered += Visualize;
        }

        private void OnDestroy() => _system.OnUserEntered -= Visualize;

        private void Visualize(IUserWithAccount user)
        {
            var userView = _userView as IUserEnterView;
            userView.Visualize(user);
            ShowMenu();
        }

        public void ShowMenu()
        {
            _userCanvas.gameObject.SetActive(false);
            _menuCanvas.gameObject.SetActive(true);
        }
    }
}