using System;

namespace CheckYourSpeed.Loging
{
    public interface ISystem
    {
        public void InviteUser();

        public void CreateNewUser();

        public event Action<IUserWithAccount> OnUserEntered;
    }
}