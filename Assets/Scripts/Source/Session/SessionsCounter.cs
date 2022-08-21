using CheckYourSpeed.Model;
using System;

namespace CheckYourSpeed.Loging
{
    public sealed class SessionsCounter : IUpdateble, ISessionCounter
    {
        private readonly ITextView _textView;
        private readonly ITimer _timer;
        private readonly IUserCounterStorage _storage;

        public SessionsCounter(ITimer timer, IUserCounterStorage storage, ITextView textView)
        {
            _timer = timer ?? throw new ArgumentNullException(nameof(timer));
            _storage = storage ?? throw new ArgumentNullException(nameof(storage));
            _textView = textView ?? throw new ArgumentNullException(nameof(textView));
            Count = storage.Load();
        }

        public int Count { get; private set; }

        private void TryIncrease()
        {
            Count++;
            _storage.Save(Count);
            _textView.Visualize(Count);
        }

        public void Update(float deltaTime)
        {
            if (_timer.FinishedCountdown)
            {
                TryIncrease();
            }
        }
    }
}