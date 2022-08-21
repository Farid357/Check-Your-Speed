namespace CheckYourSpeed.Loging
{
    public sealed class WithoutAccountUser : IUser
    {
        public bool HaveAccount(out IUserWithAccount accountableUser)
        {
            accountableUser = null;
            return false;
        }
    }
}