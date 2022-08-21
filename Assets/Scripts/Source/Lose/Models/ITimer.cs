namespace CheckYourSpeed.Model
{
    public interface ITimer : IReadOnlyTimer
    {
        public void Reset();

        public void Add(float time);

    }
}