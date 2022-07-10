﻿using TMPro;
using UnityEngine;

namespace CheckYourSpeed.Logging
{
    public sealed class UserView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _helloText;

        public void Show(User user)
        {
            _helloText.text = $"Привет, {user.Name}!";
        }
    }
}