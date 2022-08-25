using CheckYourSpeed.SaveSystem;
using NUnit.Framework;

namespace Tests
{
    public sealed class PlayerPrefsStorageTest
    {
        private const string Path = "prefs";

        [Test]
        public void PlayerPrefsStorageSavesCorrectly()
        {
            IStorage storage = new PlayerPrefsStorage();
            storage.Save(Path, 54);
            Assert.That(storage.Load<int>(Path) == 54);
        }
    }
}
