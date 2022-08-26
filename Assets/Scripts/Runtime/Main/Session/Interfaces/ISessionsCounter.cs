using UniRx;

namespace CheckYourSpeed.Loging
{
    public interface IReactiveCounter
    {
        public IReadOnlyReactiveProperty<int> Count { get; }

    }
}