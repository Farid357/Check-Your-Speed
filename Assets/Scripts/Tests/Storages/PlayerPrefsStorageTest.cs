using CheckYourSpeed.SaveSystem;
using NUnit.Framework;

namespace CheckYourSpeed.Tests.Storages
{
    public sealed class PlayerPrefsStorageTest
    {
        private const string Path = "prefs";

        [Test]
        public void PlayerPrefsStorageSavesCorrectly()
        {
            IStorage storage = new PlayerPrefsStorage();
            const int count = 54;
            storage.Save(Path, count);
            Assert.That(storage.Load<int>(Path) == count);
        }
    }
}
