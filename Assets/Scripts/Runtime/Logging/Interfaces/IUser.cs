namespace CheckYourSpeed.Loging
{
    public interface IUser
    {
        public bool HaveAccount(out IUserWithAccount accountableUser);

    }
}