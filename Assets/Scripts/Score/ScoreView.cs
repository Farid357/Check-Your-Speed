using UnityEngine;
using CheckYourSpeed.Model;
using TMPro;
using DG.Tweening;

namespace CheckYourSpeed.GameLogic
{
    public sealed class ScoreView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField] private float _scaleCofficient = 1.2f;
        [SerializeField] private float _scaleDelay = 0.6f;
        [SerializeField, ColorUsage(false)] private Color _increaseTextColor = Color.red;
        private Score _score;

        public void Init(Score score)
        {
            _score = score ?? throw new System.ArgumentNullException(nameof(score));
            _score.OnChanged += Display;
        }

        private void OnDisable() => _score.OnChanged -= Display;

        private void Display(int count)
        {
            var startScale = _text.transform.localScale;
            _text.text = count.ToString();
            _text.transform.DOScale(startScale * _scaleCofficient, _scaleDelay).
                OnComplete(new TweenCallback(() => ReturnToStartScale(_text, startScale))); 
        }

        private void ReturnToStartScale(TMP_Text text, Vector2 startScale)
        {
            text.transform.DOScale(startScale, _scaleDelay / 2f);
            var startColor = text.color;
            text.DOColor(_increaseTextColor, _scaleDelay / 2f)
                .OnComplete(new TweenCallback(() => text.DOColor(startColor, _scaleDelay / 3f)));
        }
    }
}
