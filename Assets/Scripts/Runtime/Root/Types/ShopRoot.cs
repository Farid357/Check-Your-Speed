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
        [SerializeField, RequireInterface(typeof(INotEnoughMoneyVisualization))] private INotEnoughMoneyVisualization _enoughMoneyVisualization;
        [SerializeField, RequireInterface(typeof(IVisualization<int>))] private MonoBehaviour _moneyVisualization;
        [SerializeField, RequireInterface(typeof(IVisualization<string>))] private IVisualization<string> _goodUsingVisualization;
        [SerializeField, RequireInterface(typeof(IVisualization<int>))] private IVisualization<int> _shoppingCartGoodCountVisualization;
        [SerializeField, RequireInterface(typeof(IVisualization<int>))] private IVisualization<int> _shoppingCartTotalPriceVisualization;

        [SerializeField] private SelectableGoodData[] _goodDatas;
        [SerializeField] private TMP_Text _alreadyBoughtText;

        private IUpdateble _updateble;

        public override void Compose()
        {
            var shoppingCart = new ShoppingCart(_shoppingCartGoodCountVisualization, _shoppingCartTotalPriceVisualization);
            var moneyStorage = new MoneyStorage(new BinaryStorage());
            var wallet = new Wallet(_moneyVisualization.ToInterface<IVisualization<int>>(), moneyStorage);
            var goodsStorage = new GoodsStorage(new BinaryStorage());
            var notEnoughMoneyVisualization = _enoughMoneyVisualization;
            var client = new Client(wallet, shoppingCart, goodsStorage, _enoughMoneyVisualization);
            _updateble = new GoodSelector(shoppingCart, Camera.main, client);
            _buyingButton.Init(client);


            for (int i = 0; i < _goodDatas.Length; i++)
            {
                var goodData = _goodDatas[i].Data;
                goodData.AlreadyBoughtVisualization.Init(_alreadyBoughtText);
                var good = new Good(goodData.Price, goodData.Name, _goodUsingVisualization, goodData.AlreadyBoughtVisualization);
                _goodDatas[i].Selectable.Init(goodData.Visualization, good);
            }
        }

        private void Update()
        {
            _updateble.Update(Time.deltaTime);
        }
    }
}
