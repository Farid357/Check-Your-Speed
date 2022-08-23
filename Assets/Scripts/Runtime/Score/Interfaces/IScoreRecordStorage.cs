namespace CheckYourSpeed.Model
{
    public interface IScoreRecordStorage
    {
        public int Load();

        public void Save(int count);

    }
}
