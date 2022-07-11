using CheckYourSpeed.Model;
using UnityEngine;

namespace CheckYourSpeed.Loging
{
    [CreateAssetMenu(fileName = "User Config", menuName = "Create/User Config")]
    public sealed class UserConfig : ScriptableObject, IUserConfig
    {
        private IUser _user;

        public void SetUser(IUser user)
        {
            _user = user ?? throw new System.ArgumentNullException(nameof(user));
        }

        public IUser GetUser()
        {
            if (_user == null)
                throw new System.InvalidOperationException("User is null, please set in User Config!");
            return _user;
        }
    }
}