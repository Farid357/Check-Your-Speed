namespace CheckYourSpeed.Model
{
    public interface IPointsSubscriber : IDisposable
    {
        public void Subscribe(IPoint point);
    }
}