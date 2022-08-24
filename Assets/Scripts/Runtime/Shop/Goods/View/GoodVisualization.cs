using UnityEngine;
using UnityEngine.UI;

namespace CheckYourSpeed.Shop.Visualization
{
    [RequireComponent(typeof(Image))]
    public sealed class GoodVisualization : MonoBehaviour, IGoodVisualization
    {
        [SerializeField] private Color _selected;
        private Image _image;
        private Color _startColor;

        public void Init()
        {
            _image = GetComponent<Image>();
            _startColor = _image.color;
        }

        public void Select() => SwitchColor(_selected);

        public void Unselect() => SwitchColor(_startColor);

        private void SwitchColor(Color color) => _image.color = color;

    }
}