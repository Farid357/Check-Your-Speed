using CheckYourSpeed.Model;
using UnityEngine;

namespace CheckYourSpeed.GameLogic
{
    public sealed class PointView : MonoBehaviour, IPointView
    {
        [SerializeField] private ParticleSystem _particle;
        private IPoint _point;

        public void Apply() => _point.Apply();

        public void Init(IPoint point)
        {
            _point = point;
            _point.OnApplyed += Disable;
        }

        private void OnDisable() => _point.OnApplyed -= Disable;

        private void Disable(IPoint point)
        {
            gameObject.SetActive(false);
            Instantiate(_particle, transform.position, Quaternion.identity).Play();
        }
    }
}
