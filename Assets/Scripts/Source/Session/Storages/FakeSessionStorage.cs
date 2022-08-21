namespace CheckYourSpeed.Loging
{
    public sealed class FakeSessionStorage : IUserCounterStorage
    {
        public int Load() => 0;

        public void Save(int count)
        {
           
        }
    }
}
