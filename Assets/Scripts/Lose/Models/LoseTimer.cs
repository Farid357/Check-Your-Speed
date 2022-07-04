using System;
using System.Threading.Tasks;

namespace CheckYourSpeed.Model
{
    public sealed class LoseTimer : ILoseTimer
    {
        private float _time;
        private readonly float _startTime;

        public LoseTimer(float time)
        {
            _time = time > 0 ? time : throw new ArgumentOutOfRangeException(nameof(time));
            _startTime = _time;
        }

        public event Action OnEnded;

        public void Reset()
        {
            _time = _startTime;
            Enable();
        }

        public void ResetWithAdd(float time)
        {
            _time = _startTime + time;
            Enable();
        }

        private async void Enable()
        {
            await Task.Delay(TimeSpan.FromSeconds(_time));
            _time = 0;
            OnEnded?.Invoke();
        }
    }
}