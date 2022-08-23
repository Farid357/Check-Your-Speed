using UnityEngine;
using TMPro;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using CheckYourSpeed.Model;

namespace CheckYourSpeed.Shop
{
    public sealed class GoodUsingVisualization : MonoBehaviour, IVisualization<string>
    {
        [SerializeField] private GameObject _panel;
        [SerializeField] private TMP_Text _text;
        [SerializeField] private float _visibilitySeconds = 1.2f;

        private readonly Queue<UniTask> _usingQueue = new();
        private bool _isShowing;

        private bool QueueIsNotEmpty => _usingQueue.Count != 0;

        public void Visualize(string name) => Show(_text, name);

        private async UniTask Show(TMP_Text text, string name)
        {
            if (_isShowing)
            {
                _usingQueue.Enqueue(Show(text, name));
            }

            _isShowing = true;
            text.text = name;
            _panel.SetActive(true);
            await UniTask.Delay(TimeSpan.FromSeconds(_visibilitySeconds));
            _panel.SetActive(false);
            _isShowing = false;

            if (QueueIsNotEmpty)
            {
                var last = _usingQueue.Peek();
                await last;
                _usingQueue.Dequeue();
            }
        }
    }
}