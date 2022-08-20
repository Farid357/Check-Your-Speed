using CheckYourSpeed.Model;
using System;

namespace CheckYourSpeed.Loging
{
    public sealed class SessionsCounter : IUpdateble, ISessionCounter
    {
        private readonly ITextView _textView;
        private readonly ITimer _timer;
        private readonly IUserWithAccount _user;
        private readonly SessionsCounterStorage _storage;

        public SessionsCounter(ITimer timer, IUserWithAccount user, SessionsCounterStorage storage, ITextView textView)
        {
            _timer = timer ?? throw new ArgumentNullException(nameof(timer));
            _user = user ?? throw new ArgumentNullException(nameof(user));
            _storage = storage ?? throw new ArgumentNullException(nameof(storage));
            _textView = textView ?? throw new ArgumentNullException(nameof(textView));
            Count = storage.Load(user);
        }

        public int Count { get; private set; }

        private void TryIncrease()
        {
            Count++;
            _storage.Save(_user, Count);
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