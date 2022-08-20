using System;
using System.Collections.Generic;
using System.Linq;

namespace CheckYourSpeed.Loging
{
    public sealed class System : ISystem
    {
        private readonly NameField _nameField;
        private readonly PasswordField _passwordField;
        private readonly UsersStorage _storage;

        private readonly List<User> _enteredUsers = new();

        public System(NameField nameField, PasswordField passwordField, UsersStorage storage)
        {
            _nameField = nameField ?? throw new ArgumentNullException(nameof(nameField));
            _passwordField = passwordField ?? throw new ArgumentNullException(nameof(passwordField));
            _storage = storage ?? throw new ArgumentNullException(nameof(storage));
            _enteredUsers = storage.Load();
        }

        public event Action<User> OnUserEntered;

        public void LogInNewUser()
        {
            var user = new User(_nameField.Text, _passwordField.Text);
            if (_enteredUsers.Any(enteredUser => enteredUser.Name == user.Name || enteredUser.Password == user.Password))
                throw new InvalidOperationException("System already exist same user!");

            _enteredUsers.Add(user);
            _storage.Save(_enteredUsers);
            OnUserEntered?.Invoke(user);
        }

        public void LogInUser()
        {
            var user = new User(_nameField.Text, _passwordField.Text);
            OnUserEntered?.Invoke(user);
        }
    }
}