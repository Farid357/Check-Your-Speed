using System;
using System.Collections.Generic;
using CheckYourSpeed.Shop.Model;
using CheckYourSpeed.Utils;

namespace CheckYourSpeed.Model
{
    public sealed class MoneyWithConstantChanceAdder : IPointsSubscriber
    {
        private const int MinChance = 0;
        private const int MaxChance = 10;
        private const int MoneyWithoutFactor = 5;
        
        private readonly List<IPoint> _points = new();
        private readonly IWallet _wallet;
        private readonly int _moneyFactor;

        public MoneyWithConstantChanceAdder(IWallet wallet, int moneyFactor)
        {
            _wallet = wallet ?? throw new ArgumentNullException(nameof(wallet));
            _moneyFactor = moneyFactor.TryThrowLessThanOrEqualsToZeroException();
        }

        public void Subscribe(IPoint point)
        {
            point.OnApplied += TryAdd;
            _points.Add(point);
        }

        private void TryAdd(IPoint point)
        {
            var chance = new Random().Next(MinChance, MaxChance);

            if (chance >= MaxChance / 2)
            {
                var money = MoneyWithoutFactor * _moneyFactor;
                _wallet.Put(money);
            }
        }
        
        public void Dispose() => _points.ForEach(point => point.OnApplied -= TryAdd);
        
    }
}
