using CheckYourSpeed.SaveSystem;
using NUnit.Framework;

namespace Tests
{
    public sealed class BinaryStorageTest
    {
        private const string Path = "54";

        [Test]
        public void BinaryStorageSavesCorrectly()
        {
            IStorage storage = new BinaryStorage();
            storage.Save(Path, 44);
            Assert.That(storage.Load<int>(Path) == 44);
        }
    }
}
