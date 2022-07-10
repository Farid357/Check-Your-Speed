namespace CheckYourSpeed.Logging
{
    public sealed class WithoutRegisteringUser : IUser
    {
        public bool IsAccountable => false;
    }
}