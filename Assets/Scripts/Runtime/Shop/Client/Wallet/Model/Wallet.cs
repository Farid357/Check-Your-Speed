using CheckYourSpeed.Utils;
using System;
using CheckYourSpeed.Model;
using CheckYourSpeed.SaveSystem;

namespace CheckYourSpeed.Shop.Model
{
    public sealed class Wallet : IWallet
    {
        private readonly IVisualization<int> _visualization;
        private readonly StorageWithNameSaveObject<Wallet, int> _moneyStorage;

        public Wallet(IVisualization<int> visualization, IStorage storage)
        {
            _visualization = visualization ?? throw new ArgumentNullException(nameof(visualization));
            _moneyStorage = new StorageWithNameSaveObject<Wallet, int>(storage);
            Money = _moneyStorage.HasSave() ? _moneyStorage.Load() : 25;
            _visualization.Visualize(Money);
        }
        
        public int Money { get; private set; }

        public void Put(int money)
        {
            money.TryThrowLessThanOrEqualsToZeroException();
            Money += money;
            _visualization.Visualize(Money);
            _moneyStorage.Save(Money);
        }

        public void Take(int money)
        {
            money.TryThrowLessThanOrEqualsToZeroException();
            if (CanTake(money) == false)
                throw new InvalidOperationException("Enough money!");

            Money -= money;
            _visualization.Visualize(Money);
            _moneyStorage.Save(Money);
        }

        public bool CanTake(int money) => Money - money >= 0;

    }
}