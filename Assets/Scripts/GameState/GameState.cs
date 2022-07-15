using CheckYourSpeed.Model;
using System;
using IDisposable = CheckYourSpeed.Model.IDisposable;

namespace CheckYourSpeed.App
{
    public sealed class GameState : IDisposable
    {
        private readonly PauseBroadcaster _pauseBroadcaster;
        private readonly LoseTimer _loseTimer;

        public GameState(PauseBroadcaster pauseBroadcaster, LoseTimer loseTimer)
        {
            _pauseBroadcaster = pauseBroadcaster ?? throw new ArgumentNullException(nameof(pauseBroadcaster));
            _loseTimer = loseTimer ?? throw new ArgumentNullException(nameof(loseTimer));
            _loseTimer.OnEnded += _pauseBroadcaster.Pause;
        }

        public void Dispose() => _loseTimer.OnEnded -= _pauseBroadcaster.Pause;

    }

    public interface IPauseBroadcaster
    {
        public bool IsPaused { get; }
    }
}