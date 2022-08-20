using UnityEngine;
using System;
using CheckYourSpeed.Model;

namespace CheckYourSpeed.GameLogic
{
    public sealed class PointerInput : IPointInput, IUpdateble
    {
        private readonly Camera _camera;

        public PointerInput(Camera camera)
        {
            _camera = camera ?? throw new ArgumentNullException(nameof(camera));
        }

        public event Action<IPointView> OnInputed;

        public void Update(float deltaTime)
        {

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
