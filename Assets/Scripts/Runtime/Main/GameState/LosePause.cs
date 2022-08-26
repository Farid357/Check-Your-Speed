using CheckYourSpeed.Model;
using System;

namespace CheckYourSpeed.App
{
    public sealed class LosePause : IUpdateble
    {
        private readonly IReadOnlyTimer _timer;
        private readonly IPauseSwitch _pauseSwitch;

        public LosePause(IPauseSwitch pauseSwitch, IReadOnlyTimer timer)
        {
            _pauseSwitch = pauseSwitch ?? throw new ArgumentNullException(nameof(pauseSwitch));
            _timer = timer ?? throw new ArgumentNullException(nameof(timer));
        }

        public void Update(float deltaTime)
        {
            if (_timer.FinishedCountdown)
            {
                _pauseSwitch.Pause();
            }
        }
    }

    public interface IPauseBroadcaster
    {
        public bool GameIsPaused { get; }
    }
}