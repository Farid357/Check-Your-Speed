using System.Collections.Generic;

namespace CheckYourSpeed.Shop
{
    public interface IShoppingCart : IReadOnlyShoppingCart
    {
        public void Add(IGood good);

        public void Remove(IGood good);

    }

    public interface IReadOnlyShoppingCart
    {
        public int GetTotalPrice();

        public IEnumerable<IGood> Goods { get; }

    }
}