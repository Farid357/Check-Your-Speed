using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using System;
using DG.Tweening;

namespace CheckYourSpeed.Shop
{
    public sealed class NotEnoughMoneyVisualization : MonoBehaviour, INotEnoughMoneyVisualization
    {
        [SerializeField] private float _fillAmountSeconds = 1.2f;
        [SerializeField] private Image _image;

        public void Visualize() => ShowImage();

        private async UniTask ShowImage()
        {
            _image.DOFillAmount(1, _fillAmountSeconds);
            await UniTask.Delay(TimeSpan.FromSeconds(_fillAmountSeconds));
            _image.DOFillAmount(0, _fillAmountSeconds);
        }
    }
}