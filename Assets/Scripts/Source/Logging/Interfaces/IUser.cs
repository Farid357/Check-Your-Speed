namespace CheckYourSpeed.Loging
{
    public interface IUser
    {
        public bool CanHaveAccount(out IUserWithAccount accountableUser)
        {
            if (this is IUserWithAccount user)
            {
                accountableUser = user;
                return true;
            }

            else
            {
                accountableUser = null;
                return false;
            }
        }
    }
}