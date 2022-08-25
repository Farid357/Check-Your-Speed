using CheckYourSpeed.Utils;
using System;
using System.Collections.Generic;
using CheckYourSpeed.SaveSystem;

namespace CheckYourSpeed.Shop.Model
{
    public sealed class Client : IClient
    {
        private readonly IWallet _wallet;
        private readonly IReadOnlyShoppingCart _shoppingCart;
        private readonly INotEnoughMoneyVisualization _notEnoughMoney;

        private readonly StorageWithNameSaveObject<List<IGood>> _goodsStorage;
        private readonly List<IGood> _boughtGoods;

        public Client(IWallet wallet, IReadOnlyShoppingCart shoppingCart,
            INotEnoughMoneyVisualization visualization)
        {
            _wallet = wallet ?? throw new ArgumentNullException(nameof(wallet));
            _shoppingCart = shoppingCart ?? throw new ArgumentNullException(nameof(shoppingCart));
            _notEnoughMoney = visualization ?? throw new ArgumentNullException(nameof(visualization));
            _goodsStorage = new StorageWithNameSaveObject<List<IGood>>();
            _boughtGoods = _goodsStorage.HasSave() ? _goodsStorage.Load() : new();
        }

        public void Buy()
        {
            var totalPrice = _shoppingCart.GetTotalPrice();

            if (_boughtGoods.ContainsElementFrom(_shoppingCart.Goods))
                throw new InvalidOperationException("This item is already bought!");

            if (_wallet.CanTake(totalPrice))
            {
                _wallet.Take(totalPrice);
                _boughtGoods.AddRange(_shoppingCart.Goods);
                _shoppingCart.Goods.ForEach(good => good.Use());
                _goodsStorage.Save(_boughtGoods);
            }

            else
            {
                _notEnoughMoney.Visualize();
            }
        }

        public bool HasBought(IGood good) => _boughtGoods.Contains(good);
    }
}