using TMPro;
using UnityEngine;
using DG.Tweening;

namespace CheckYourSpeed.Loging
{
    public sealed class UserEnterView : MonoBehaviour, IUserEnterView
    {
        [SerializeField] private TMP_Text _helloText;
        [SerializeField] private float _delay = 1.8f;

        public void Visualize(IUserWithAccount user)
        {
            _helloText.DOText($"Привет, {user.Name}!", _delay, scrambleMode: ScrambleMode.Lowercase);
        }
    }
}