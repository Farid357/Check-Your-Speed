using CheckYourSpeed.Utils;
using System;
using CheckYourSpeed.Model;

namespace CheckYourSpeed.Shop.Model
{
    public sealed class Good : IGood
    {
        private readonly IVisualization<string> _visualization;
        private readonly IGoodIsBuyedVisualization _isBuyedVisualization;

        public Good(int price, string name, IVisualization<string> visualization, IGoodIsBuyedVisualization isBuyedVisualization)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"\"{nameof(name)}\" не может быть пустым или содержать только пробел.", nameof(name));
            }

            _visualization = visualization ?? throw new ArgumentNullException(nameof(visualization));
            _isBuyedVisualization = isBuyedVisualization ?? throw new ArgumentNullException(nameof(isBuyedVisualization));
            Price = price.TryThrowLessThanOrEqualsToZeroException();
            Name = name;
        }

        public int Price { get; }

        public string Name { get; }

        public void Use()
        {
            _visualization.Visualize(Name);
            _isBuyedVisualization.Visualize();
        }
    }
}