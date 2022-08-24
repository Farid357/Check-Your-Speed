namespace CheckYourSpeed.Shop.Model
{
    public interface IShoppingCart : IReadOnlyShoppingCart
    {
        public void Add(IGood good);

        public void Remove(IGood good);

    }
}