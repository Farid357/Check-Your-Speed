using UnityEngine;
using TMPro;
using Cysharp.Threading.Tasks;
using System;
using DG.Tweening;
using UnityEngine.UI;
using CheckYourSpeed.Shop.Model;

namespace CheckYourSpeed.Shop.Visualization
{
    [RequireComponent(typeof(Image))]
    public sealed class GoodAlreadyBoughtVisualization : MonoBehaviour, IGoodIsBuyedVisualization
    {
        [SerializeField, Min(0.1f)] private float _visibilitySeconds = 1.2f;
        [SerializeField, Min(0.1f)] private float _textIncreaseScale = 1.5f;
        [SerializeField, Min(0.1f)] private float _textIncreaseScaleSeconds = 0.6f;
        [SerializeField] private Color _buyed = Color.white;
        [SerializeField] private Image _lock;
        private TMP_Text _text;

        public void Init(TMP_Text text) => _text = text ?? throw new ArgumentNullException(nameof(text));

        public void Visualize()
        {
            var image = GetComponent<Image>();
            image.color = _buyed;
            _lock.gameObject.SetActive(false);
        }

        public void Select() => VisualizeIsBuying();

        public void Unselect()
        {
           
        }


        private async UniTask VisualizeIsBuying()
        {
            var startTextScale = _text.transform.localScale;
            _text.gameObject.SetActive(true);
            _text.transform.DOScale(startTextScale * _textIncreaseScale, _textIncreaseScaleSeconds);
            await UniTask.Delay(TimeSpan.FromSeconds(_visibilitySeconds + _textIncreaseScale));
            _text.transform.DOScale(startTextScale, _textIncreaseScaleSeconds / 2f);
            _text.gameObject.SetActive(false);
        }
    }
}