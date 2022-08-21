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
        private PauseBroadcaster _pauseBroadcaster;
        private PointPointerInput _input;

        public void Init(PauseBroadcaster pauseBroadcaster)
        {
            _pauseBroadcaster = pauseBroadcaster ?? throw new System.ArgumentNullException(nameof(pauseBroadcaster));
            _input = new PointPointerInput(Camera.main);
        }

        public void Compose()
        {
            _pointsInAreaSpawner.Init(_input);
            _pauseButton.Init(_pauseBroadcaster);
            _continueButton.Init(_pauseBroadcaster);
        }

        private void Update()
        {
            if (_pauseBroadcaster.GameIsPaused == false)
                _input.Update(Time.deltaTime);
        }
    }
}