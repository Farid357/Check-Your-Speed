using UnityEngine;
using CheckYourSpeed.Utils;
using System;
using System.Collections.Generic;
using CheckYourSpeed.Model;

namespace CheckYourSpeed.Shop
{
    public sealed class GoodSelector : IUpdateble
    {
        private readonly Camera _camera;
        private readonly IShoppingCart _shoppingCart;
        private readonly IClient _client;
        private readonly List<ISelectableGood> _goods = new();

        public GoodSelector(IShoppingCart shoppingCart, Camera camera, IClient client)
        {
            _shoppingCart = shoppingCart ?? throw new ArgumentNullException(nameof(shoppingCart));
            _client = client ?? throw new ArgumentNullException(nameof(client));
            _camera = camera ?? throw new ArgumentNullException(nameof(camera));
        }

        public void Update(float deltaTime)
        {
            if (Input.GetMouseButtonDown(0))
            {
                var mousePosistion = _camera.ScreenToWorldPoint(Input.mousePosition);
                var raycast = Physics2D.Raycast(mousePosistion, Vector2.zero);

                if (raycast.Hit<ISelectableGood>(out var selectableGood) == false)
                    return;

                var good = selectableGood.Good;
                Debug.Log("e");
                if (_goods.Contains(selectableGood) == false)
                {
                    Select(selectableGood, good);
                }

                else
                {
                    Unselect(selectableGood, good);
                }
            }
        }

        private void Select(ISelectableGood selectableGood, IGood good)
        {
            selectableGood.Select();
            _goods.Add(selectableGood);
            if (_client.IsBuyed(good))
            {
                selectableGood.VisualizeBuying();
            }
            else
            {
                _shoppingCart.Add(good);
            }
        }

        private void Unselect(ISelectableGood selectableGood, IGood good)
        {
            selectableGood.Unselect();
            _goods.Remove(selectableGood);
            _shoppingCart.Remove(good);
        }

    }
}