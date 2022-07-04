namespace CheckYourSpeed.Model
{
    public interface ILoseTimer
    {
        public void Reset();

        public void ResetWithAdd(float time);

    }
}