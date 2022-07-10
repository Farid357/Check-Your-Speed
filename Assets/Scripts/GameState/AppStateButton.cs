using CheckYourSpeed.Model;
using UnityEngine;
using UnityEngine.UI;

namespace CheckYourSpeed.App
{
    [RequireComponent(typeof(Button))]
    public abstract class AppStateButton : MonoBehaviour
    {
        private GameState _gameState;
        private Button _button;

        public void Init(GameState gameState)
        {
            _gameState = gameState ?? throw new System.ArgumentNullException(nameof(gameState));
            _button = GetComponent<Button>();
            _button.onClick.AddListener(() => OnClick(_gameState));
        }

        protected abstract void OnClick(GameState gameState);
    }
}
