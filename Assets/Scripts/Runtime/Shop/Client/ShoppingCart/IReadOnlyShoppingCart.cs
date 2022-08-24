using System.Collections.Generic;

namespace CheckYourSpeed.Shop.Model
{
    public interface IReadOnlyShoppingCart
    {
        public int GetTotalPrice();

        public IEnumerable<IGood> Goods { get; }

    }
}