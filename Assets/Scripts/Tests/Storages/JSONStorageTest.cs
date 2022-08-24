using CheckYourSpeed.SaveSystem;
using NUnit.Framework;

namespace Tests
{
    public sealed class JSONStorageTest
    {
        private const string Path = "11441.txt";

        [Test]
        public void JSONStorageSavesCorrectly()
        {
            IStorage storage = new JSONStorage();
            storage.Save(Path, 44);
            Assert.That(storage.Load<int>(Path) == 44);
        }
    }
}
