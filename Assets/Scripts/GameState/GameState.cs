using CheckYourSpeed.Model;
using System;

namespace CheckYourSpeed.App
{
    public sealed class GameState : IUpdateble
    {
        private readonly ITimer _loseTimer;
        private readonly IPauseSwitch _pauseSwitch;

        public GameState(IPauseSwitch pauseSwitch, ITimer loseTimer)
        {
            _pauseSwitch = pauseSwitch ?? throw new ArgumentNullException(nameof(pauseSwitch));
            _loseTimer = loseTimer ?? throw new ArgumentNullException(nameof(loseTimer));
        }

        public void Update(float deltaTime)
        {
            if (_loseTimer.FinishedCountdown)
            {
                _pauseSwitch.Pause();
            }
        }
    }

    public interface IPauseBroadcaster
    {
        public bool IsPaused { get; }
    }
}