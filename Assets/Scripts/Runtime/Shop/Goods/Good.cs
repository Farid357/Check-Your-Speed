using CheckYourSpeed.Utils;
using System;
using CheckYourSpeed.Model;

namespace CheckYourSpeed.Shop
{
    public sealed class Good : IGood
    {
        private readonly IVisualization<string> _visualization;

        public Good(int price, string name, IVisualization<string> visualization)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"\"{nameof(name)}\" не может быть пустым или содержать только пробел.", nameof(name));
            }

            _visualization = visualization ?? throw new ArgumentNullException(nameof(visualization));
            Price = price.TryThrowLessOrEqualZeroException();
            Name = name;
        }

        public int Price { get; }

        public string Name { get; }

        public void Use()
        {
            _visualization.Visualize(Name);
        }
    }
}