using UnityEngine;

namespace CheckYourSpeed.Shop
{
    [RequireComponent(typeof(SpriteRenderer))]
    public sealed class GoodVisualization : MonoBehaviour, IGoodVisualization
    {
        [SerializeField] private Color _selected;
        private SpriteRenderer _spriteRenderer;
        private Color _startColor;

        public void Init()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _startColor = _spriteRenderer.color;
        }

        public void Select() => SwitchColor(_selected);

        public void Unselect() => SwitchColor(_startColor);

        private void SwitchColor(Color color) => _spriteRenderer.color = color;

    }
}