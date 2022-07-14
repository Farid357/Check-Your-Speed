namespace CheckYourSpeed.Model
{
    public interface IPointVisitor
    {
        public void Visit(ScorePoint scorePoint);

        public void Visit(WavePoint wavePoint);

        public void Visit(DisablePoint disablePoint);

        public void Visit(MultiplePoint multiplePoint);

    }
}