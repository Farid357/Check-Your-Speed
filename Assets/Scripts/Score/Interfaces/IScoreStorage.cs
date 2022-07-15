namespace CheckYourSpeed.Model
{
    public interface IScoreStorage
    {
        public Score Load();

        public void Save(Score score);

    }
}
