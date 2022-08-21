using System;

namespace CheckYourSpeed.Loging
{
    [Serializable]
    public sealed class User : IUser, IUserWithAccount
    {
        public User(string name, string password)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"\"{nameof(name)}\" не может быть пустым или содержать только пробел.", nameof(name));
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException($"\"{nameof(password)}\" не может быть пустым или содержать только пробел.", nameof(password));
            }

            Name = name;
            Password = password;
        }

        public string Name { get; }

        public string Password { get; }

        public bool HaveAccount(out IUserWithAccount accountableUser)
        {
            accountableUser = this;
            return true;
        }
    }
}