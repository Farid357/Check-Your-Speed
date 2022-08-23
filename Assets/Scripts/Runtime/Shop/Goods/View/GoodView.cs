using UnityEngine;
using UnityEngine.UI;
using System;

namespace CheckYourSpeed.Shop
{
    [RequireComponent(typeof(SpriteRenderer))]
    public sealed class GoodView : MonoBehaviour, IGoodView
    {
        [SerializeField] private Image _lock;
        [SerializeField] private Color _selectedOrBuyed;
        private SpriteRenderer _spriteRenderer;
        private Color _startColor;

        private bool _clientIsBuyedGood;

        public IGood Good { get; private set; }

        public void Init(IClient client, IGood good)
        {
            Good = good ?? throw new ArgumentNullException(nameof(good));
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _startColor = _spriteRenderer.color;
            _clientIsBuyedGood = client.IsBuyed(good);
            _lock.gameObject.SetActive(_clientIsBuyedGood);
            _spriteRenderer.color = _clientIsBuyedGood ? _selectedOrBuyed : _startColor;
        }

        public void Select() => SwitchColor(_selectedOrBuyed);

        public void UnSelect() => SwitchColor(_startColor);

        private void SwitchColor(Color color) => _spriteRenderer.color = color;

    }
}