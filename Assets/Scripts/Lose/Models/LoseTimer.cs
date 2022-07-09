using System;
using System.Text;
using TMPro;
using UnityEngine;

namespace CheckYourSpeed.Model
{
    public sealed class LoseTimer : ILoseTimer, IUpdatable
    {
        private float _time;
        private readonly float _startTime;

        public LoseTimer(float time)
        {
            _time = time > 0 ? time : throw new ArgumentOutOfRangeException(nameof(time));
            _startTime = _time;
        }

        public event Action OnEnded;

        public void Reset() => _time = _startTime;

        public void ResetWithAdd(float time) => _time = _startTime + time;

        public void Update(float deltaTime)
        {
            _time -= deltaTime;

            if (_time <= 0)
            {
                OnEnded?.Invoke();
            }
        }
    }

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

        public bool IsAccountable => false;
    }

    public sealed class WithoutRegisteringUser : IUser
    {
        public bool IsAccountable => false;
    }

    public sealed class UserView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _helloText;

        public void Show(User user)
        {
            _helloText.text = $"Привет, {user.Name}!";
        }
    }
}