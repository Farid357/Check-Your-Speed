using UnityEngine;
using UnityEngine.UI;

namespace CheckYourSpeed.Loging
{
    public sealed class HelpBox : MonoBehaviour, IHelpBox
    {
        [SerializeField] private GameObject _helpBox;
        [SerializeField] private Image _checkMark;

        public void VisualizeError() => Display(false);

        public void VisualizeCorrect() => Display(true);

        private void Display(bool valid)
        {
            _helpBox.SetActive(!valid);
            _checkMark.gameObject.SetActive(valid);
        }
    }
}