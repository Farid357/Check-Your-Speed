using System;
using UnityEngine;

namespace CheckYourSpeed.Loging
{
    [CreateAssetMenu(fileName = "User Config", menuName = "Create/User Config")]
    public sealed class UserConfig : ScriptableObject, IUserConfig
    {
        private IUser _user;

        public void SaveUser(IUser user)
        {
            _user = user ?? throw new ArgumentNullException(nameof(user));
        }

        public IUser LoadUser()
        {
            if (_user == null)
                throw new InvalidOperationException("User is null, please set in User Config!");
            return _user;
        }
    }
}