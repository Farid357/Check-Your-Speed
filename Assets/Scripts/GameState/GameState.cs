using CheckYourSpeed.Model;
using System;
using IDisposable = CheckYourSpeed.Model.IDisposable;

namespace CheckYourSpeed.App
{
    public sealed class GameState : IDisposable
    {
        private readonly LoseTimer _loseTimer;

        public GameState(LoseTimer loseTimer)
        {
            _loseTimer = loseTimer ?? throw new ArgumentNullException(nameof(loseTimer));
            _loseTimer.OnEnded += Pause;
        }

        public bool IsPaused { get; private set; }

        public void Pause() => IsPaused = true;

        public void UnPause() => IsPaused = false;

        public void Dispose() => _loseTimer.OnEnded -= Pause;

    }
}