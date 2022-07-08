using System;

namespace CheckYourSpeed.Model
{
    public sealed class LoseTimer : ILoseTimer, IUpdatable
    {
        private float _time;
        private readonly float _startTime;

        public LoseTimer(float time)
        {
            _time = time > 0 ? time : throw new ArgumentOutOfRangeException(nameof(time));
            _startTime = _time;
        }

        public event Action OnEnded;

        public void Reset() => _time = _startTime;

        public void ResetWithAdd(float time) => _time = _startTime + time;

        public void Update(float deltaTime)
        {
            _time -= deltaTime;

            if (_time <= 0)
            {
                OnEnded?.Invoke();
            }
        }
    }
}