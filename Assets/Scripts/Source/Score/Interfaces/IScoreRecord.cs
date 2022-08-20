using UniRx;

namespace CheckYourSpeed.Model
{
    public interface IScoreRecord
    {
        public IReadOnlyReactiveProperty<int> Count { get; }
    }
}
