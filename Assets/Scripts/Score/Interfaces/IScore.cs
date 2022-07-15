using UniRx;

namespace CheckYourSpeed.Model
{
    public interface IScore
    {
        public IReadOnlyReactiveProperty<int> Count { get; }
    }
}
