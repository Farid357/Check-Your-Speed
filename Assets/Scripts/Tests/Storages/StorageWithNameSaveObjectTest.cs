using CheckYourSpeed.SaveSystem;
using NUnit.Framework;

namespace CheckYourSpeed.Tests.Storages
{
    public sealed class StorageWithNameSaveObjectTest
    {
        [Test]
        public void StorageWithNameSaveObjectSaveCorrectly()
        {
            var storage = new StorageWithNameSaveObject<StorageWithNameSaveObjectTest, int>();
            const int count = 44;
            storage.Save(count);
            Assert.That(storage.Load() == count);
        }
    }
}
