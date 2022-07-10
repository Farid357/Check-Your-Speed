namespace CheckYourSpeed.Loging
{
    public sealed class WithoutRegisteringUser : IUser
    {
        public bool IsAccountable => false;
    }
}