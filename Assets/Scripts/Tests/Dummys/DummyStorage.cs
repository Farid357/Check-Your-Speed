using CheckYourSpeed.SaveSystem;

namespace CheckYourSpeed.Tests
{
    public sealed class DummyStorage : IStorage
    {
        public T Load<T>(string key) => default;

        public void Save<T>(string key, T saveObject)
        {
        }

        public bool Exists(string name) => false;
    }
}