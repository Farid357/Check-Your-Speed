using Cysharp.Threading.Tasks;
using System;
using TMPro;
using UnityEngine;

namespace CheckYourSpeed.Loging
{
    public sealed class NotFoundUserView : MonoBehaviour, INotFoundUserView
    {
        [SerializeField] private float _showDelay = 1.8f;
        [SerializeField] private TMP_Text _text;

        public void StartVisualize() => Visualize();

        private async UniTaskVoid Visualize()
        {
            _text.gameObject.SetActive(true);
            await UniTask.Delay(TimeSpan.FromSeconds(_showDelay));
            _text.gameObject.SetActive(false);
        }
    }
}