using System;
using System.Collections.Generic;
using System.Linq;
using CheckYourSpeed.SaveSystem;

namespace CheckYourSpeed.Loging
{
    public sealed class System : ISystem
    {
        private readonly INameField _nameField;
        private readonly IPasswordField _passwordField;
        private readonly StorageWithNameSaveObject<System, List<IUserWithAccount>> _storage;

        private readonly List<IUserWithAccount> _enteredUsers;

        public System(INameField nameField, IPasswordField passwordField, IStorage storage)
        {
            _nameField = nameField ?? throw new ArgumentNullException(nameof(nameField));
            _passwordField = passwordField ?? throw new ArgumentNullException(nameof(passwordField));
            _storage = new StorageWithNameSaveObject<System, List<IUserWithAccount>>(storage);
            _enteredUsers = _storage.HasSave() ? _storage.Load() : new();
        }

        public IReadOnlyList<IUserWithAccount> EnteredUsers => _enteredUsers;

        public event Action<IUserWithAccount> OnUserEntered;

        public void CreateNewUser()
        {
            IUserWithAccount user = new User(_nameField.Text, _passwordField.Text);
            if (_enteredUsers.Any(enteredUser => enteredUser.Name == user.Name || enteredUser.Password == user.Password))
                throw new InvalidOperationException("System already exist same user!");

            _enteredUsers.Add(user);
            _storage.Save(_enteredUsers);
            OnUserEntered?.Invoke(user);
        }

        public void InviteUser()
        {
            var user = new User(_nameField.Text, _passwordField.Text);
            OnUserEntered?.Invoke(user);
        }
    }
}