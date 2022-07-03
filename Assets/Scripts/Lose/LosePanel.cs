using UnityEngine;

namespace CheckYourSpeed.GameLogic
{
    public sealed class LosePanel : MonoBehaviour
    {
        public void Show()
        {
            gameObject.SetActive(true);
        }
    }
}