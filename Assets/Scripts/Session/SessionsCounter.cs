using CheckYourSpeed.Model;
using System;

namespace CheckYourSpeed.Loging
{
    public sealed class SessionsCounter : IUpdateble
    {
        private readonly IUser _user;
        private readonly SessionsCounterStorage _storage;
        private readonly ITextView _textView;
        private readonly Timer _loseTimer;
        private int _count;
        private bool _hasNotIncreased = true;

        public SessionsCounter(Timer loseTimer, IUser user, SessionsCounterStorage storage, ITextView textView)
        {
            _loseTimer = loseTimer ?? throw new ArgumentNullException(nameof(loseTimer));
            _user = user ?? throw new ArgumentNullException(nameof(user));
            _storage = storage ?? throw new ArgumentNullException(nameof(storage));
            _textView = textView ?? throw new ArgumentNullException(nameof(textView));
            _count = storage.Load();
        }

        private void TryIncrease()
        {
            _hasNotIncreased = false;
            _count++;
            _textView.Display(_count);

            if (_user.IsAccountable)
            {
                _storage.Save(_user, _count);
            }
        }

        public void Update(float deltaTime)
        {
            if (_loseTimer.FinishedCountdown && _hasNotIncreased)
            {
                TryIncrease();
            }
        }
    }
}