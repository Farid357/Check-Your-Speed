using UnityEngine;
using UnityEngine.UI;
using System;
using CheckYourSpeed.Shop.Model;

namespace CheckYourSpeed.Shop
{
    [RequireComponent(typeof(Button))]
    public sealed class BuyingButton : MonoBehaviour
    {
        private Button _button;
        private IClient _client;

        public void Init(IClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
            _button = GetComponent<Button>();
            _button.onClick.AddListener(Buy);
        }

        private void Buy() => _client.Buy();


        private void OnDestroy() => _button.onClick.RemoveListener(Buy);

    }
}