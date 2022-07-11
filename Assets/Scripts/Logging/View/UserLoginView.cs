using UnityEngine;

namespace CheckYourSpeed.Loging
{
    public sealed class UserLogInView : MonoBehaviour
    {
        [SerializeField] private Canvas _userCanvas;
        [SerializeField] private Canvas _menuCanvas;
        [SerializeField] private UserView _userView;
        private IUserFinder _userFinder;

        public void Init(IUserFinder userFinder)
        {
            _userFinder = userFinder ?? throw new System.ArgumentNullException(nameof(userFinder));
            _userFinder.OnFoundUser += Display;
        }

        private void OnDestroy() => _userFinder.OnFoundUser -= Display;

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