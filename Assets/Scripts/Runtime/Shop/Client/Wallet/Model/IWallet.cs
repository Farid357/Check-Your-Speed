namespace CheckYourSpeed.Shop.Model
{
    public interface IWallet
    {

        public void Put(int money);

        public void Take(int money);

        public bool CanTake(int money);
    }
}