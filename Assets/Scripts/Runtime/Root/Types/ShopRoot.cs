using CheckYourSpeed.Model;
using CheckYourSpeed.SaveSystem;
using CheckYourSpeed.Shop;
using CheckYourSpeed.Utils;
using UnityEngine;
using System.Linq;

namespace CheckYourSpeed.Root
{
    public sealed class ShopRoot : CompositeRoot
    {
        [SerializeField] private BuyingButton _buyingButton;
        [SerializeField, RequireInterface(typeof(INotEnoughMoneyVisualization))] private INotEnoughMoneyVisualization _enoughMoneyVisualization;
        [SerializeField, RequireInterface(typeof(IGoodIsBuyedVisualization))] private IGoodIsBuyedVisualization _goodIsBuyedVisualization;
        [SerializeField, RequireInterface(typeof(IVisualization<int>))] private MonoBehaviour _moneyVisualization;
        [SerializeField, RequireInterface(typeof(IVisualization<string>))] private IVisualization<string> _goodUsingVisualization;

        private IUpdateble _updateble;

        public override void Compose()
        {
            var goodViews = FindObjectsOfType<GoodView>().ToList();
            var shoppingCart = new ShoppingCart();
            var moneyStorage = new MoneyStorage(new BinaryStorage());
            var wallet = new Wallet(_moneyVisualization.ToInterface<IVisualization<int>>(), moneyStorage);
            var goodsStorage = new GoodsStorage(new BinaryStorage());
            var notEnoughMoneyVisualization = _enoughMoneyVisualization;
            var client = new Client(wallet, shoppingCart, goodsStorage, _enoughMoneyVisualization);
            _updateble = new GoodSelector(shoppingCart, Camera.main, client, _goodIsBuyedVisualization);
            goodViews.ForEach(good => good.Init(client, new Good(50, "Good", _goodUsingVisualization)));
            _buyingButton.Init(client);
        }

        private void Update()
        {
            _updateble.Update(Time.deltaTime);
        }
    }
}
