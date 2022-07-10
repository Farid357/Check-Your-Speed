using System;

namespace CheckYourSpeed.Logging
{
    [Serializable]
    public sealed class User : IUser
    {
        public readonly string Name;
        public readonly string Password;

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

        public bool IsAccountable => true;
    }
}