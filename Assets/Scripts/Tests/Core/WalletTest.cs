using CheckYourSpeed.Shop.Model;
using NUnit.Framework;

namespace CheckYourSpeed.Tests
{
    public sealed class WalletTest
    {
        [Test]
        public void WalletMethodCanTakeWorksCorrectly()
        {
            var wallet = new Wallet(new DummyCountVisualization(), new DummyStorage());
            Assert.That(wallet.CanTake(25));
        }
        
        [Test]
        public void WalletMethodTakeWorksCorrectly()
        {
            var wallet = new Wallet(new DummyCountVisualization(), new DummyStorage());
            wallet.Take(25);
            Assert.That(wallet.Money == 0);
        }

        [Test]
        public void WalletVisualizeCountCorrectly()
        {
            var visualization = new DummyCountVisualization();
            var wallet = new Wallet(visualization, new DummyStorage());
            wallet.Take(1);
            Assert.That(visualization.Count == 24);
        }
    }
}
