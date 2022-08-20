using CheckYourSpeed.Model;
using System;
using UnityEngine;

namespace CheckYourSpeed.GameLogic
{
    [RequireComponent(typeof(CircleCollider2D), typeof(SpriteRenderer))]
    public sealed class PointView : MonoBehaviour, IPointView
    {
        [SerializeField] private ParticleSystem _particle;
        private IPoint _point;
        private SpriteRenderer _spriteRenderer;
        private Action _onDisabled;

        public bool Enable => gameObject.activeInHierarchy;

        public Vector3 Position => transform.position;

        public event Action OnDisabled { add => _onDisabled = value; remove => _onDisabled -= value; }

        private void Awake() => _spriteRenderer ??= GetComponent<SpriteRenderer>();

        private void OnDisable()
        {
            if (_point != null)
                _point.OnApplied -= Disable;
        }

        public void Init(IPoint point, Color color)
        {
            _point = point ?? throw new ArgumentNullException(nameof(point));
            _point.OnApplied += Disable;
            _spriteRenderer.color = color;
        }

        public void Apply() => _point.Apply();

        public void Disable()
        {
            Instantiate(_particle, transform.position, Quaternion.identity).Play();
            gameObject.SetActive(false);
        }

        private void Disable(IPoint point)
        {
            _onDisabled?.Invoke();
            Disable();
        }
    }
}
