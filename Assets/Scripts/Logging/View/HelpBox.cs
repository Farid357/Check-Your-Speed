using UnityEngine;
using UnityEngine.UI;

namespace CheckYourSpeed.Logging
{
    public sealed class HelpBox : MonoBehaviour
    {
        [SerializeField] private FakeLogging _logging;
        [SerializeField] private GameObject _helpBox;
        [SerializeField] private Image _checkMark;

        private void OnEnable()
        {
            _logging.OnFoundInvalidSymbols += DisplayBox;
            _logging.OnLogged += DisplayCheckMark;
        }

        private void OnDisable()
        {
            _logging.OnFoundInvalidSymbols -= DisplayBox;
            _logging.OnLogged -= DisplayCheckMark;
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