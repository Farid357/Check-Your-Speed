using UnityEngine;
using CheckYourSpeed.App;
using CheckYourSpeed.GameLogic;

namespace CheckYourSpeed.Root
{
    public sealed class InputRoot : MonoBehaviour
    {
        [SerializeField] private PauseButton _pauseButton;
        [SerializeField] private ContinueButton _continueButton;
        [SerializeField] private PointsInAreaSpawner _pointsInAreaSpawner;
        private GameState _gameState;
        private PointerInput _input;

        public void Init(GameState gameState)
        {
            _gameState = gameState ?? throw new System.ArgumentNullException(nameof(gameState));
            _input = new PointerInput(Camera.main, gameState);
        }

        public void Compose()
        {
            _pointsInAreaSpawner.Init(_input);
            _pauseButton.Init(_gameState);
            _continueButton.Init(_gameState);
        }

        private void Update() => _input.Update(Time.deltaTime);

    }
}