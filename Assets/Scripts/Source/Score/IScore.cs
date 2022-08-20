namespace CheckYourSpeed.Model
{
    public interface IScore
    {
        public int Count { get; }

        public bool WasCountChanged { get; }
    }
}