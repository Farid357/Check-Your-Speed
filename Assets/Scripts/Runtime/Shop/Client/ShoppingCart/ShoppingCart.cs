using CheckYourSpeed.Model;
using System;
using System.Collections.Generic;

namespace CheckYourSpeed.Shop
{
    public sealed class ShoppingCart : IShoppingCart
    {
        private readonly List<IGood> _goods = new();
        private readonly IVisualization<int> _visualization;

        public ShoppingCart(IVisualization<int> visualization)
        {
            _visualization = visualization ?? throw new ArgumentNullException(nameof(visualization));
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
            if (good is null)
            {
                throw new ArgumentNullException(nameof(good));
            }

            _goods.Add(good);
            _visualization.Visualize(_goods.Count);
        }

        public void Remove(IGood good)
        {
            if (good is null)
            {
                throw new ArgumentNullException(nameof(good));
            }

            if (_goods.Contains(good) == false)
            {
                throw new InvalidOperationException("Good list is not contains this good!");
            }

            _goods.Remove(good);
            _visualization.Visualize(_goods.Count);
        }

    }
}