using UnityEngine;
using System;
using UnityEngine.UI;
using DG.Tweening;

namespace CheckYourSpeed.GameLogic
{
    public sealed class WaveSpawnerView : MonoBehaviour
    {
        [SerializeField] private Image _waiting;
        [SerializeField] private Image _startWave;
        [SerializeField] private float _scaleCofficient = 1.4f;
        private IWaveSpawner _waveSpawner;

        public void Init(IWaveSpawner waveSpawner)
        {
            _waveSpawner = waveSpawner ?? throw new ArgumentNullException(nameof(waveSpawner));
            _waveSpawner.OnWaiting += ShowWaitingImage;
            _waveSpawner.OnSpawningNextWave += ShowStartWave;
        }

        private void OnDisable()
        {
            _waveSpawner.OnWaiting -= ShowWaitingImage;
            _waveSpawner.OnSpawningNextWave -= ShowStartWave;
        }

        private void ShowWaitingImage()
        {
            var startScale = _waiting.transform.localScale;
            _waiting.gameObject.SetActive(true);
            _waiting.transform.DOScale(startScale * _scaleCofficient, _scaleCofficient / 2)
                .OnComplete(new TweenCallback(() => _waiting.transform.localScale = startScale));
        }

        private void ShowStartWave()
        {
            _startWave.color = new Color(_startWave.color.r, _startWave.color.g, _startWave.color.b, 1);
            _waiting.gameObject.SetActive(false);
            _startWave.gameObject.SetActive(true);
            _startWave.DOFade(0, _scaleCofficient);
        }
    }
}
