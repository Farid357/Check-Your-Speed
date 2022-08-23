using CheckYourSpeed.Model;
using CheckYourSpeed.Utils;
using System;
using UnityEngine;

namespace CheckYourSpeed.GameLogic
{
    [RequireComponent(typeof(CircleCollider2D), typeof(SpriteRenderer))]
    public sealed class PointView : MonoBehaviour, IPointView
    {
        [SerializeField] private ParticleSystem _particle;
        private IPoint _point;
        private IPool<PointView> _pool;
        private Action _onDisabled;

        public bool Enable => gameObject.activeInHierarchy;

        public Vector3 Position => transform.position;

        public event Action OnDisabled { add => _onDisabled = value; remove => _onDisabled -= value; }

        private void OnDisable() => _pool.Release(this);

        public void Init(IPoint point, IPool<PointView> pool, Color color)
        {
            _point = point ?? throw new ArgumentNullException(nameof(point));
            _pool = pool ?? throw new ArgumentNullException(nameof(pool));
            GetComponent<SpriteRenderer>().color = color;
        }

        public void Apply()
        {
            _point.Apply();
            _onDisabled?.Invoke();
            Disable();
        }

        public void Disable()
        {
            Instantiate(_particle, transform.position, Quaternion.identity).Play();
            gameObject.SetActive(false);
        }
    }
}
