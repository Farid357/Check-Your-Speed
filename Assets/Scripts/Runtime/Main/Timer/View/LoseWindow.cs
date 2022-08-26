using UnityEngine;

namespace CheckYourSpeed.GameLogic
{
    public sealed class LoseWindow : MonoBehaviour
    {
        public void Show() => gameObject.SetActive(true);

    }
}