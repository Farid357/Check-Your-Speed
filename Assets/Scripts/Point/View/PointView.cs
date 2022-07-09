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

        public event Action OnDisabled { add => _onDisabled = value; remove => _onDisabled -= value; }

        private void Awake() => _spriteRenderer = GetComponent<SpriteRenderer>();

        private void OnDestroy()
        {
            if (_point != null)
                _point.OnApplyed -= Disable;
        }

        public void Init(IPoint point)
        {
            _point = point ?? throw new ArgumentNullException(nameof(point));
            _point.OnApplyed += Disable;
        }
        public void Apply() => _point.Apply();

        public void SetColor(Color color) => _spriteRenderer.color = color;


        private void Disable(IPoint point)
        {
            Instantiate(_particle, transform.position, Quaternion.identity).Play();
            Disable();
        }

        public void Disable()
        {
            gameObject.SetActive(false);
            _onDisabled?.Invoke();
        }
    }
}
