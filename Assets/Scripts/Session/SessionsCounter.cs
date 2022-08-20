using CheckYourSpeed.Model;
using System;

namespace CheckYourSpeed.Loging
{
    public sealed class SessionsCounter : IUpdateble, ISessionCounter
    {
        private readonly IUser _user;
        private readonly ITextView _textView;
        private readonly ITimer _timer;
        private readonly SessionsCounterStorage _storage;
        private bool _hasNotIncreased = true;

        public SessionsCounter(ITimer timer, IUser user, SessionsCounterStorage storage, ITextView textView)
        {
            _timer = timer ?? throw new ArgumentNullException(nameof(timer));
            _user = user ?? throw new ArgumentNullException(nameof(user));
            _storage = storage ?? throw new ArgumentNullException(nameof(storage));
            _textView = textView ?? throw new ArgumentNullException(nameof(textView));
            Count = storage.Load();
        }

        public int Count { get; private set; }

        private void TryIncrease()
        {
            _hasNotIncreased = false;
            Count++;
            _textView.Visualize(Count);

            if (_user.IsAccountable)
            {
                _storage.Save(_user, Count);
            }
        }

        public void Update(float deltaTime)
        {
            if (_timer.FinishedCountdown && _hasNotIncreased)
            {
                TryIncrease();
            }
        }
    }
}