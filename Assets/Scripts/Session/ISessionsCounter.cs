using UniRx;

namespace CheckYourSpeed.Loging
{
    public interface ISessionsCounter
    {
        public IReadOnlyReactiveProperty<int> Count { get; }
    }
}