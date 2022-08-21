using CheckYourSpeed.Model;
using NUnit.Framework;

namespace CheckYourSpeed.Tests
{
    public sealed class WavePointFactoryTest
    {
        [Test]
        public void IsFactoryCreateNeedType()
        {
            var factory = new WavePointFactory(new DummyTimer(), new DummyPointsSwitch());
            Assert.That(factory.Create() is WavePoint);
        }
    }
}
