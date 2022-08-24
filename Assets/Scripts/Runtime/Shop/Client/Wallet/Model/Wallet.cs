using CheckYourSpeed.Utils;
using System;
using CheckYourSpeed.Model;

namespace CheckYourSpeed.Shop.Model
{
    public sealed class Wallet : IWallet
    {
        private readonly IVisualization<int> _visualization;
        private readonly MoneyStorage _moneyStorage;
        private int _money;

        public Wallet(IVisualization<int> visualization, MoneyStorage moneyStorage)
        {
            _visualization = visualization ?? throw new ArgumentNullException(nameof(visualization));
            _moneyStorage = moneyStorage ?? throw new ArgumentNullException(nameof(moneyStorage));
            _money = _moneyStorage.HasSave() ? _moneyStorage.Load() : 25;
        }

        public void Put(int money)
        {
            money.TryThrowLessOrEqualsToZeroException();
            _money += money;
            _visualization.Visualize(money);
            _moneyStorage.Save(money);
        }

        public void Take(int money)
        {
            money.TryThrowLessOrEqualsToZeroException();
            if (CanTake(money) == false)
                throw new InvalidOperationException("Enough money!");

            _money -= money;
            _visualization.Visualize(money);
            _moneyStorage.Save(money);
        }

        public bool CanTake(int money) => _money - money >= 0;

    }
}