namespace CheckYourSpeed.Loging
{
    public interface IUserConfig
    {
        public bool TryLoad(out IUserWithAccount userWithAccount);
    }
}