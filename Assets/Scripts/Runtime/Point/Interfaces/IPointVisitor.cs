namespace CheckYourSpeed.Model
{
    public interface IPointVisitor
    {
        public void Visit(TimerPoint timerPoint);

        public void Visit(WavePoint wavePoint);

        public void Visit(DisablePoint disablePoint);

        public void Visit(MultiplePoint multiplePoint);

        public void Visit(RandomPoint randomPoint);
    }
}