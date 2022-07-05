namespace CheckYourSpeed.Model
{
    public sealed class ScorePoint : Point
    {
        public ScorePoint(ILoseTimer timer) : base(timer)
        {
        }

        protected override void PlayApplyFeedback()
        {

        }
    }
}