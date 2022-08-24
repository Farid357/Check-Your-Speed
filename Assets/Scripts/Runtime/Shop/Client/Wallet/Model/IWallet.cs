namespace CheckYourSpeed.Shop
{
    public interface IWallet
    {

        public void Put(int money);

        public void Take(int money);

        public bool TryTake(int money);
    }
}