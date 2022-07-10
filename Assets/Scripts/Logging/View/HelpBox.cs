using UnityEngine;
using UnityEngine.UI;

namespace CheckYourSpeed.Loging
{
    public sealed class HelpBox : MonoBehaviour
    {
        [SerializeField] private LogInField _logging;
        [SerializeField] private GameObject _helpBox;
        [SerializeField] private Image _checkMark;

        private void OnEnable()
        {
            _logging.OnFoundInvalidSymbols += DisplayBox;
            _logging.OnWrote += DisplayCheckMark;
        }

        private void OnDisable()
        {
            _logging.OnFoundInvalidSymbols -= DisplayBox;
            _logging.OnWrote -= DisplayCheckMark;
        }

        private void DisplayBox() => Display(false);

        private void DisplayCheckMark() => Display(true);

        private void Display(bool valid)
        {
            _helpBox.gameObject.SetActive(!valid);
            _checkMark.gameObject.SetActive(valid);
        }
    }
}