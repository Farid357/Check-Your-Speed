using UnityEngine;
using System;
using CheckYourSpeed.Model;
using CheckYourSpeed.App;

namespace CheckYourSpeed.GameLogic
{
    public sealed class PointerInput : IInput, IUpdatable
    {
        private readonly Camera _camera;
        private readonly GameState _gameState;

        public PointerInput(Camera camera, GameState gameState)
        {
            _camera = camera ?? throw new ArgumentNullException(nameof(camera));
            _gameState = gameState ?? throw new ArgumentNullException(nameof(gameState));
        }

        public event Action<IPointView> OnInputed;

        public void Update(float deltaTime)
        {
            if (_gameState.IsPaused)
                return;

            if (Input.GetMouseButtonDown(0))
            {
                var mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
                var hit = Physics2D.Raycast(mousePosition, Vector2.zero);

                if (hit.collider != null)
                {
                    if (hit.collider.TryGetComponent(out IPointView point))
                    {
                        point.Apply();
                        OnInputed?.Invoke(point);
                    }
                }
            }
        }

    }
}
