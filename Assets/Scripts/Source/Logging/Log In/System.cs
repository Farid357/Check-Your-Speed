using System;
using System.Collections.Generic;
using System.Linq;

namespace CheckYourSpeed.Loging
{
    public sealed class System : ISystem
    {
        private readonly INameField _nameField;
        private readonly IPasswordField _passwordField;
        private readonly UsersStorage _storage;

        public readonly List<IUserWithAccount> EnteredUsers;

        public System(INameField nameField, IPasswordField passwordField, UsersStorage storage)
        {
            _nameField = nameField ?? throw new ArgumentNullException(nameof(nameField));
            _passwordField = passwordField ?? throw new ArgumentNullException(nameof(passwordField));
            _storage = storage ?? throw new ArgumentNullException(nameof(storage));
            EnteredUsers = storage.Load();
        }

        public event Action<IUserWithAccount> OnUserEntered;

        public void CreateNewUser()
        {
            IUserWithAccount user = new User(_nameField.Text, _passwordField.Text);
            if (EnteredUsers.Any(enteredUser => enteredUser.Name == user.Name || enteredUser.Password == user.Password))
                throw new InvalidOperationException("System already exist same user!");

            EnteredUsers.Add(user);
            _storage.Save(EnteredUsers);
            OnUserEntered?.Invoke(user);
        }

        public void InviteUser()
        {
            var user = new User(_nameField.Text, _passwordField.Text);
            OnUserEntered?.Invoke(user);
        }
    }
}