using Cysharp.Threading.Tasks;
using System;
using TMPro;
using UnityEngine;

namespace CheckYourSpeed.Loging
{
    [RequireComponent(typeof(TMP_Text))]
    public sealed class NotFoundUserView : MonoBehaviour, INotFoundUserView
    {
        [SerializeField] private float _showDelay = 1.8f;
        private TMP_Text _text;

        private void Awake() => _text = GetComponent<TMP_Text>();

        public void StartVisualize() => Visualize();

        private async UniTaskVoid Visualize()
        {
            _text.gameObject.SetActive(true);
            await UniTask.Delay(TimeSpan.FromSeconds(_showDelay));
            _text.gameObject.SetActive(false);
        }
    }
}