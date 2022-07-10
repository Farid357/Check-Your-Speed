using UnityEngine;

namespace CheckYourSpeed.Logging
{
    public sealed class UserLogginView : MonoBehaviour
    {
        [SerializeField] private Canvas _userCanvas;
        [SerializeField] private Canvas _menuCanvas;
        [SerializeField] private UserView _userView;
        [SerializeField] private UserLoggIn _loggIn;

        private void OnEnable() => _loggIn.OnFoundUser += Display;

        private void OnDisable() => _loggIn.OnFoundUser -= Display;

        private void Display(User user)
        {
            _userView.Show(user);
            ShowMenu();
        }

        public void DisableUserUI() => ShowMenu();

        private void ShowMenu()
        {
            _userCanvas.gameObject.SetActive(false);
            _menuCanvas.gameObject.SetActive(true);
        }
    }
}