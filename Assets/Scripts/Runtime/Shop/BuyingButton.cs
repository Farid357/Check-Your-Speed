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
        private IShoppingCart _shoppingCart;

        public void Init(IClient client, IShoppingCart shoppingCart)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
            _shoppingCart = shoppingCart ?? throw new ArgumentNullException(nameof(shoppingCart));
            _button = GetComponent<Button>();
            _button.onClick.AddListener(Buy);
        }

        private void Buy()
        {
            _client.Buy();
            _shoppingCart.Clear();
        }


        private void OnDestroy() => _button.onClick.RemoveListener(Buy);
    }
}