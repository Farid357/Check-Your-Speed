using UnityEngine;
using System;
using CheckYourSpeed.Model;
using CheckYourSpeed.App;

namespace CheckYourSpeed.GameLogic
{
    public sealed class PointerInput : IPointInput, IUpdateble
    {
        private readonly IPauseBroadcaster _pauseBroadcaster;
        private readonly Camera _camera;

        public PointerInput(Camera camera, IPauseBroadcaster pause)
        {
            _camera = camera ?? throw new ArgumentNullException(nameof(camera));
            _pauseBroadcaster = pause ?? throw new ArgumentNullException(nameof(pause));
        }

        public event Action<IPointView> OnInputed;

        public void Update(float deltaTime)
        {
            if (_pauseBroadcaster.IsPaused)
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
