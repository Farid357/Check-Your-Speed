using CheckYourSpeed.Model;
using UnityEngine;

namespace CheckYourSpeed.GameLogic
{
    [RequireComponent(typeof(CircleCollider2D), typeof(SpriteRenderer))]
    public sealed class PointView : MonoBehaviour, IPointView
    {
        [SerializeField] private ParticleSystem _particle;
        private IPoint _point;
        private SpriteRenderer _spriteRenderer;

        public void Apply() => _point.Apply();

        private void Awake() => _spriteRenderer = GetComponent<SpriteRenderer>();

        public void Init(IPoint point)
        { 
            _point = point ?? throw new System.ArgumentNullException(nameof(point));
            _point.OnApplyed += Disable;
        }

        public void SetColor(Color color) => _spriteRenderer.color = color;

        private void OnDestroy() => _point.OnApplyed -= Disable;

        private void Disable(IPoint point)
        {
            gameObject.SetActive(false);
            Instantiate(_particle, transform.position, Quaternion.identity).Play();
        }
    }
}
