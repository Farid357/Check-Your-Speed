using System.Collections;
using UnityEngine;
using CheckYourSpeed.Utils;
using CheckYourSpeed.Model;
using System;

namespace CheckYourSpeed.GameLogic
{
    public sealed class WaveSpawner : MonoBehaviour, IWaveCleaner, IWaveSpawner
    {
        [SerializeField] private float _delay = 0.5f;
        [SerializeField] private int _startCount = 5;
        [SerializeField] private PointView _prefab;
        [SerializeField] private Waves _waves;
        [SerializeField] private PointColorProvider _provider;
        private ILoseTimer _loseTimer;
        private IPointsSubscriber _pointsSubscriber;
        private Vector2[] _positions;
        private Wave _currentWave;
        private ObjectPool<PointView> _pool;

        public event Action OnWaitingNextWave;
        public event Action OnSpawningNextWave;

        private IWavesContainer Waves => _waves;

        public void Init(IPointsSubscriber pointsSubscriber, ILoseTimer loseTimer, Vector2[] positions)
        {
            _loseTimer = loseTimer ?? throw new ArgumentNullException(nameof(loseTimer));
            _positions = positions ?? throw new ArgumentNullException(nameof(positions));
            _pointsSubscriber = pointsSubscriber ?? throw new ArgumentNullException(nameof(pointsSubscriber));
            _pool = new ObjectPool<PointView>(_startCount, _prefab, transform);
            StartCoroutine(Spawn(_prefab));
        }

        private IEnumerator Spawn(PointView prefab)
        {
            var wait = new WaitForSeconds(_delay);

            while (true)
            {
                _currentWave = Waves.Get();
                var count = _currentWave.PointsCountInWave;

                while (count > 0)
                {
                    for (int i = 0; i < _currentWave.PointsCountOnScreen; i++)
                    {
                        yield return wait;
                        var pointView = _pool.Get(prefab);
                        var randomPoint = GetRandomPoint(_currentWave);
                        _pointsSubscriber.Subscribe(randomPoint);
                        pointView.transform.position = new Vector2().GetRandomPointFrom(_positions);
                        pointView.gameObject.SetActive(true);
                        pointView.SetColor(_provider.Get(randomPoint));
                        pointView.Init(randomPoint);
                        count--;
                    }
                }

                OnWaitingNextWave?.Invoke();
                _loseTimer.ResetWithAdd(_currentWave.DelayAfterEnd);
                yield return new WaitForSeconds(_currentWave.DelayAfterEnd);
                OnSpawningNextWave?.Invoke();
            }
        }

        private IPoint GetRandomPoint(Wave wave)
        {
            var randomIndex = UnityEngine.Random.Range(0, wave.Points.Length);
            return wave.Points[randomIndex];
        }

        public void CleanWave()
        {
            if (_currentWave.Points == null)
                throw new InvalidOperationException();
            Waves.RemoveFirst();
            StartCoroutine(Spawn(_prefab));
        }
    }
}
