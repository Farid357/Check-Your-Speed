using System;
using UnityEngine;

namespace CheckYourSpeed.Loging
{
    [CreateAssetMenu(fileName = "User Config", menuName = "Create/User Config")]
    public sealed class UserConfig : ScriptableObject, IUserConfig
    {
        private IUser _user;
        private IUserWithAccount _userWithAccount;

        public void SaveUser(IUser user)
        {
            _user = user ?? throw new ArgumentNullException(nameof(user));
        }

        public void SaveUser(IUserWithAccount userWithAccount)
        {
            _userWithAccount = userWithAccount ?? throw new ArgumentNullException(nameof(userWithAccount));
            _user = null;
        }

        public bool TryLoad(out IUserWithAccount userWithAccount)
        {
            if (_user == null && _userWithAccount == null)
                throw new InvalidOperationException("User is null, please save in User Config!");

            if (_user != null)
            {
                userWithAccount = null;
                return false;
            }

            else
            {
                userWithAccount = _userWithAccount;
                return true;
            }
        }
    }
}