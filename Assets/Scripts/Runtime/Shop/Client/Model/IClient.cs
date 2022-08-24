namespace CheckYourSpeed.Shop
{
    public interface IClient
    {
        public void Buy();

        public bool IsBuyed(IGood good);

    }
}