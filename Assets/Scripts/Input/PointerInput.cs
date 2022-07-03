using UnityEngine;
using System;

namespace CheckYourSpeed.GameLogic
{
    [Serializable]
    public sealed class PointerInput : IInput, IUpdatable
    {
        private Camera _camera;

        public PointerInput(Camera camera)
        {
            _camera = camera ?? throw new ArgumentNullException(nameof(camera));
        }

        public PointerInput() { }

        public event Action<PointView> OnInputed;

        public void Update(float deltaTime)
        {
            if (Input.GetMouseButtonDown(0))
            {
                var mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
                var hit = Physics2D.Raycast(mousePosition, Vector2.zero);

                if (hit.collider.TryGetComponent(out PointView point))
                {
                    point.Apply();
                    OnInputed?.Invoke(point);
                }
            }
        }

    }
}
