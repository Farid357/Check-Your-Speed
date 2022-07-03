using System.Collections;
using UnityEngine;
using CheckYourSpeed.Utils;
using System.Linq;
using CheckYourSpeed.Model;

namespace CheckYourSpeed.GameLogic
{
    public sealed class PointsSpawner : MonoBehaviour, IWaveCleaner
    {
        [SerializeField] private float _delay = 0.5f;
        [SerializeField] private int _startCount = 5;
        [SerializeField] private PointView _prefab;
        [SerializeField] private Waves _waves;

        private PointsPositionsSpawner _positionsSpawner;
        private IPointsSubscriber _pointsSubscriber;
        private Wave _currentWave;
        private ObjectPool<PointView> _pool;

        public void Init(IPointsSubscriber pointsSubscriber, PointsPositionsSpawner positionsSpawner)
        {
            _positionsSpawner = positionsSpawner ?? throw new System.ArgumentNullException(nameof(positionsSpawner));
            _pointsSubscriber = pointsSubscriber ?? throw new System.ArgumentNullException(nameof(pointsSubscriber));
            _pool = new ObjectPool<PointView>(_startCount, _prefab, transform);
            StartCoroutine(Spawn(_prefab));
        }

        private IEnumerator Spawn(PointView prefab)
        {
            var wait = new WaitForSeconds(_delay);

            while (true)
            {
                _currentWave = _waves.Get();
                var count = _currentWave.PointsCountInWave;

                while (count > 0)
                {
                    for (int i = 0; i < _currentWave.PointsCountOnScreen; i++)
                    {
                        yield return wait;
                        var pointView = _pool.Get(prefab);
                        var positions = _positionsSpawner.Positions.ToArray();
                        var randomPoint = GetRandomPoint(_currentWave);
                        _pointsSubscriber.Subscribe(randomPoint);
                        pointView.transform.position = new Vector2().GetRandomPointFrom(positions);
                        pointView.gameObject.SetActive(true);
                        pointView.Init(randomPoint);
                        count--;
                    }
                    yield return null;
                }

                yield return new WaitForSeconds(_currentWave.DelayAfterEnd);
            }
        }

        private IPoint GetRandomPoint(Wave wave)
        {
            var randomIndex = Random.Range(0, wave.Points.Length);
            return wave.Points[randomIndex];
        }

        public void CleanWave()
        {
            if (_currentWave.Points == null)
                throw new System.InvalidOperationException();
            _waves.RemoveFirst();
            StartCoroutine(Spawn(_prefab));
        }
    }
}
