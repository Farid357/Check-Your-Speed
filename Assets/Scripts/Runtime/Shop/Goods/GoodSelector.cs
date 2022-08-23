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
        private readonly IGoodIsBuyedVisualization _goodIsBuyedVisualization;
        private readonly IShoppingCart _shoppingCart;
        private readonly IClient _client;
        private readonly List<IGoodView> _goods = new();

        public GoodSelector(IShoppingCart shoppingCart, Camera camera, IClient client, IGoodIsBuyedVisualization goodIsBuyedVisualization)
        {
            _shoppingCart = shoppingCart ?? throw new ArgumentNullException(nameof(shoppingCart));
            _client = client ?? throw new ArgumentNullException(nameof(client));
            _camera = camera ?? throw new ArgumentNullException(nameof(camera));
            _goodIsBuyedVisualization = goodIsBuyedVisualization ?? throw new ArgumentNullException(nameof(goodIsBuyedVisualization));
        }

        public void Update(float deltaTime)
        {
            if (Input.GetMouseButtonDown(0))
            {
                var mousePosistion = _camera.ScreenToWorldPoint(Input.mousePosition);
                var raycast = Physics2D.Raycast(mousePosistion, Vector2.zero);

                if (raycast.Hit<IGoodView>(out var goodView))
                {
                    var good = goodView.Good;

                    if (_goods.Contains(goodView) == false)
                    {
                        if(_client.IsBuyed(good))
                        {
                            _goodIsBuyedVisualization.Visualize();
                            return;
                        }
                        goodView.Select();
                        _goods.Add(goodView);
                        _shoppingCart.Add(good);
                    }

                    else
                    {
                        goodView.UnSelect();
                        _goods.Remove(goodView);
                        _shoppingCart.Remove(good);
                    }
                }
            }
        }
    }
}