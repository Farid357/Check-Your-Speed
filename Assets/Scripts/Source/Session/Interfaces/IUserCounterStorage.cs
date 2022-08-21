namespace CheckYourSpeed.Loging
{
    public interface IUserCounterStorage
    {
        public int Load();

        public void Save(int count);
    }
}