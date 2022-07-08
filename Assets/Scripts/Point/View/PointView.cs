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

        public event Action OnDisabled;

        public void Apply() => _point.Apply();

        private void Awake() => _spriteRenderer = GetComponent<SpriteRenderer>();

        public void Init(IPoint point)
        { 
            _point = point ?? throw new ArgumentNullException(nameof(point));
            _point.OnApplyed += Disable;
        }

        public void SetColor(Color color) => _spriteRenderer.color = color;

        private void OnDestroy() => _point.OnApplyed -= Disable;

        private void Disable(IPoint point)
        {
            gameObject.SetActive(false);
            Instantiate(_particle, transform.position, Quaternion.identity).Play();
            OnDisabled?.Invoke();
        }

        public void Disable()
        {
            gameObject.SetActive(false);
            OnDisabled?.Invoke();
        }
    }
}
