namespace CheckYourSpeed.Shop.Model
{
    public interface IClient
    {
        public void Buy();

        public bool IsBuyed(IGood good);

    }
}