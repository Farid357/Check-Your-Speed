using System;

namespace CheckYourSpeed.Loging
{
    public interface ISystem
    {
        public void LogInUser();

        public void LogInNewUser();

        public event Action<User> OnUserEntered;
    }
}