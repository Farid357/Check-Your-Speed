using TMPro;
using UnityEngine;
using DG.Tweening;

namespace CheckYourSpeed.Loging
{
    public sealed class UserView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _helloText;
        [SerializeField] private float _delay = 1.8f;

        public void Show(User user)
        {
            _helloText.DOText($"Привет, {user.Name}!", _delay, scrambleMode: ScrambleMode.Lowercase);
        }
    }
}