using CheckYourSpeed.SaveSystem;
using NUnit.Framework;

namespace Tests
{
    public sealed class JSONStorageTest
    {
        private const string Path = "xd.json";

        [Test]
        public void JSONStorageSavesCorrectly()
        {
            IStorage storage = new JSONStorage();
            storage.Save(Path, 45);
            Assert.That(storage.Load<int>(Path) == 44);
        }
    }
}
