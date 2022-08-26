using CheckYourSpeed.SaveSystem;
using NUnit.Framework;

namespace CheckYourSpeed.Tests.Storages
{
    public sealed class JSONStorageTest
    {
        private const string Path = "mono";

        [Test]
        public void JSONStorageSavesCorrectly()
        {
            IStorage storage = new JsonStorage();
            const int count = 45;
            storage.Save(Path, count);
            Assert.That(storage.Load<int>(Path) == count);
        }
    }
}
