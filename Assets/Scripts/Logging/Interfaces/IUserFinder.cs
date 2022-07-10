using System;

namespace CheckYourSpeed.Loging
{
    public interface IUserFinder
    {
        public event Action<User> OnFoundUser;
    }
}