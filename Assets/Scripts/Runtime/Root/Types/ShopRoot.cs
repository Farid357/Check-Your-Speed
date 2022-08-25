using CheckYourSpeed.Model;
using CheckYourSpeed.SaveSystem;
using CheckYourSpeed.Shop;
using CheckYourSpeed.Shop.Data;
using CheckYourSpeed.Shop.Model;
using CheckYourSpeed.Utils;
using TMPro;
using UnityEngine;

namespace CheckYourSpeed.Root
{
    public sealed class ShopRoot : CompositeRoot
    {
        [SerializeField] private BuyingButton _buyingButton;
        [SerializeField, RequireInterface(typeof(INotEnoughMoneyVisualization))] private MonoBehaviour _notEnoughMoneyVisualization;
        [SerializeField, RequireInterface(typeof(IVisualization<int>))] private MonoBehaviour _moneyVisualization;
        [SerializeField, RequireInterface(typeof(IVisualization<string>))] private MonoBehaviour _goodUsingVisualization;
        [SerializeField, RequireInterface(typeof(IVisualization<int>))] private MonoBehaviour _shoppingCartGoodCountVisualization;
        [SerializeField, RequireInterface(typeof(IVisualization<int>))] private MonoBehaviour _shoppingCartTotalPriceVisualization;
        [SerializeField] private GoodSpawner _goodSpawner;
        [SerializeField] private SelectableGoodData[] _goodDatas;
        [SerializeField] private TMP_Text _alreadyBoughtText;

        private IUpdateble _goodSelector;

        public override void Compose()
        {
            var shoppingCart = new ShoppingCart((IVisualization<int>)_shoppingCartGoodCountVisualization, (IVisualization<int>)_shoppingCartTotalPriceVisualization);
            var wallet = new Wallet(_moneyVisualization.ToInterface<IVisualization<int>>());
            var client = new Client(wallet, shoppingCart, (INotEnoughMoneyVisualization)_notEnoughMoneyVisualization);
            _goodSelector = new GoodSelector(shoppingCart, Camera.main, client);
            _buyingButton.Init(client);


            foreach (var data in _goodDatas)
            {
                var goodData = data.Data;
                var goodVisualization = _goodSpawner.Spawn(goodData.Visualization);
                goodVisualization.Init(goodData.Sprite);
                var good = new Good(goodData.Price, goodData.Name, (IVisualization<string>)_goodUsingVisualization, goodData.AlreadyBoughtVisualization);
                if (client.HasBought(good))
                    goodData.AlreadyBoughtVisualization.Init(_alreadyBoughtText);
                data.Selectable.Init(goodVisualization, good);
            }
        }

        private void Update()
        {
            _goodSelector.Update(Time.deltaTime);
        }
    }
}
