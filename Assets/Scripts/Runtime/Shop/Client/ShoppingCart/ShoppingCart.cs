using CheckYourSpeed.Model;
using System;
using System.Collections.Generic;

namespace CheckYourSpeed.Shop.Model
{
    public sealed class ShoppingCart : IShoppingCart
    {
        private readonly IVisualization<int> _visualization;
        private readonly IVisualization<int> _totalPriceVisualization;
        private readonly List<IGood> _goods = new();

        public ShoppingCart(IVisualization<int> countVisualization, IVisualization<int> totalPriceVisualization)
        {
            _visualization = countVisualization ?? throw new ArgumentNullException(nameof(countVisualization));
            _totalPriceVisualization = totalPriceVisualization ?? throw new ArgumentNullException(nameof(totalPriceVisualization));
        }

        public IEnumerable<IGood> Goods => _goods;

        public int GetTotalPrice()
        {
            var price = 0;
            _goods.ForEach(good => price += good.Price);
            return price;
        }

        public void Add(IGood good)
        {
            good = good ?? throw new ArgumentNullException(nameof(good));
            _goods.Add(good);
            VisualizeAll();
        }

        public void Remove(IGood good)
        {
            good = good ?? throw new ArgumentNullException(nameof(good));
            _goods.Remove(good);
            VisualizeAll();
        }
        
        public void Clear()
        {
            _goods.Clear();
            VisualizeAll();
        }

        private void VisualizeAll()
        {
            _visualization.Visualize(_goods.Count);
            _totalPriceVisualization.Visualize(GetTotalPrice());
        }
    }
}