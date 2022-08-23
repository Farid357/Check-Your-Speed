using CheckYourSpeed.Utils;
using System;
using System.Collections.Generic;

namespace CheckYourSpeed.Shop
{
    public sealed class Client : IClient
    {
        private readonly IWallet _wallet;
        private readonly IReadOnlyShoppingCart _shoppingCart;
        private readonly INotEnoughMoneyVisualization _notEnoughMoney;

        private readonly GoodsStorage _goodsStorage;
        private readonly List<IGood> _buyedGoods;

        public Client(IWallet wallet, IReadOnlyShoppingCart shoppingCart, GoodsStorage goodsStorage, INotEnoughMoneyVisualization visualization)
        {
            _wallet = wallet ?? throw new ArgumentNullException(nameof(wallet));
            _shoppingCart = shoppingCart ?? throw new ArgumentNullException(nameof(shoppingCart));
            _goodsStorage = goodsStorage ?? throw new ArgumentNullException(nameof(goodsStorage));
            _notEnoughMoney = visualization ?? throw new ArgumentNullException(nameof(visualization));
            _buyedGoods = _goodsStorage.HasSave() ? _goodsStorage.Load() : new();
        }

        public void Buy()
        {
            var totalPrice = _shoppingCart.GetTotalPrice();

            if (_buyedGoods.ContainsElementFrom(_shoppingCart.Goods))
                throw new InvalidOperationException("This item is already buyed!");

            if (_wallet.TryTake(totalPrice))
            {
                _wallet.Take(totalPrice);
                _buyedGoods.AddRange(_shoppingCart.Goods);
                _shoppingCart.Goods.ForEach(good => good.Use());
                _goodsStorage.Save(_buyedGoods);
            }

            else
            {
                _notEnoughMoney.Visualize();
            }
        }

        public bool IsBuyed(IGood good) => _buyedGoods.Contains(good);

    }
}