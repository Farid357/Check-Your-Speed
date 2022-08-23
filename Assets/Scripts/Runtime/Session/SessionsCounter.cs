using CheckYourSpeed.Model;
using System;

namespace CheckYourSpeed.Loging
{
    public sealed class SessionsCounter : IUpdateble, ISessionCounter
    {
        private readonly IVisualization<int> _visualization;
        private readonly ITimer _timer;
        private readonly IUserCounterStorage _storage;

        public SessionsCounter(ITimer timer, IUserCounterStorage storage, IVisualization<int> visualization)
        {
            _timer = timer ?? throw new ArgumentNullException(nameof(timer));
            _storage = storage ?? throw new ArgumentNullException(nameof(storage));
            _visualization = visualization ?? throw new ArgumentNullException(nameof(visualization));
            Count = storage.Load();
            visualization.Visualize(Count);
        }

        public int Count { get; private set; }

        private void TryIncrease()
        {
            Count++;
            _storage.Save(Count);
            _visualization.Visualize(Count);
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