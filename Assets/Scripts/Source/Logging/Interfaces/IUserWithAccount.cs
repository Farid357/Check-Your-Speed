namespace CheckYourSpeed.Loging
{
    public interface IUserWithAccount : IUser
    {
        public string Name { get; }
        public string Password { get; }

    }
}