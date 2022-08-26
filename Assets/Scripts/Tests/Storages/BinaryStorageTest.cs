using CheckYourSpeed.SaveSystem;
using NUnit.Framework;

namespace CheckYourSpeed.Tests.Storages
{
    public sealed class BinaryStorageTest
    {
        private const string Path = "54";

        [Test]
        public void BinaryStorageSavesCorrectly()
        {
            IStorage storage = new BinaryStorage();
            const int count = 44;
            storage.Save(Path, count);
            Assert.That(storage.Load<int>(Path) == count);
        }
    }
}
