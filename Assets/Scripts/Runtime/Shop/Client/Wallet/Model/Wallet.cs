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
        private int _money;

        public Wallet(IVisualization<int> visualization)
        {
            _visualization = visualization ?? throw new ArgumentNullException(nameof(visualization));
            _moneyStorage = new StorageWithNameSaveObject<Wallet, int>();
            _money = _moneyStorage.HasSave() ? _moneyStorage.Load() : 25;
            _visualization.Visualize(_money);
        }

        public void Put(int money)
        {
            money.TryThrowLessThanOrEqualsToZeroException();
            _money += money;
            _visualization.Visualize(_money);
            _moneyStorage.Save(_money);
        }

        public void Take(int money)
        {
            money.TryThrowLessThanOrEqualsToZeroException();
            if (CanTake(money) == false)
                throw new InvalidOperationException("Enough money!");

            _money -= money;
            _visualization.Visualize(_money);
            _moneyStorage.Save(_money);
        }

        public bool CanTake(int money) => _money - money >= 0;

    }
}