using CheckYourSpeed.SaveSystem;
using NUnit.Framework;

namespace Tests
{
    public sealed class PlayerPrefsStorageTest
    {
        private const string Path = "104.txt";

        [Test]
        public void PlayerPrefsStorageSavesCorrectly()
        {
            IStorage storage = new PlayerPrefsStorage();
            storage.Save(Path, 44);
            Assert.That(storage.Load<int>(Path) == 44);
        }
    }
}
