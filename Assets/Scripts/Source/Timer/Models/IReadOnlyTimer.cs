namespace CheckYourSpeed.Model
{
    public interface IReadOnlyTimer
    {
        public bool FinishedCountdown { get; }

        public float Time { get; }
    }
}