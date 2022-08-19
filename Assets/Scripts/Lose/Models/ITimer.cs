namespace CheckYourSpeed.Model
{
    public interface ITimer
    {
        public void Reset();

        public void ResetWithAdd(float time);

    }
}