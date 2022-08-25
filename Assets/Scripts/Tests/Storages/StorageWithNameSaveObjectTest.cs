using CheckYourSpeed.SaveSystem;
using NUnit.Framework;

namespace CheckYourSpeed.Tests
{
    public sealed class StorageWithNameSaveObjectTest
    {
        [Test]
        public void StorageWithNameSaveObjectSaveCorrectly()
        {
            var storage = new StorageWithNameSaveObject<int>();
            storage.Save(44);
            Assert.That(storage.Load() == 44);
        }
    }
}
