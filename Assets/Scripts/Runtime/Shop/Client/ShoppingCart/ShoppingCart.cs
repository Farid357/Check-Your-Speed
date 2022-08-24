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
            Validate(good);
            _goods.Add(good);
            _visualization.Visualize(_goods.Count);
            _totalPriceVisualization.Visualize(GetTotalPrice());
        }

        public void Remove(IGood good)
        {
            Validate(good);
            _goods.Remove(good);
            _visualization.Visualize(_goods.Count);
            _totalPriceVisualization.Visualize(GetTotalPrice());
        }

        private void Validate(IGood good)
        {
            if (good is null)
            {
                throw new ArgumentNullException(nameof(good));
            }
        }
    }
}