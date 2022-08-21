namespace CheckYourSpeed.Loging
{
    public sealed class FakeUserCounterStorage : IUserCounterStorage
    {
        public int Load() => 0;

        public void Save(int count)
        {
           
        }
    }
}
