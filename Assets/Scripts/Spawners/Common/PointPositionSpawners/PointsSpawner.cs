using UnityEngine;
using CheckYourSpeed.Utils;
using CheckYourSpeed.Model;
using System;
using System.Collections.Generic;

namespace CheckYourSpeed.GameLogic
{
    public abstract class PointsSpawner : MonoBehaviour, IPointsSpawner
    {
        [SerializeField] private PointView _prefab;
        [SerializeField] private int _startCount = 5;
        [SerializeField] private PointColorProvider _provider;
        private readonly List<IPointView> _spawnedPoints = new();
        private IPointsSubscriber _pointsSubscriber;
        private Waves _waves;
        private ObjectPool<PointView> _pool;

        public IEnumerable<IPointView> SpawnedPoints => _spawnedPoints;

        public event Action<IPointView> OnSpawned;

        public void Init(IPointsSubscriber pointsSubscriber, Waves waves)
        {
            _pointsSubscriber = pointsSubscriber ?? throw new ArgumentNullException(nameof(pointsSubscriber));
            _waves = waves ?? throw new ArgumentNullException(nameof(waves));
            _pool = new ObjectPool<PointView>(_startCount, _prefab, transform);
        }

        public void Spawn(Factory.PointType[] pointTypes)
        {
            var pointView = _pool.Get(_prefab);
            var randomPoint = _waves.GetRandomPoint(pointTypes);
            _pointsSubscriber.Subscribe(randomPoint);
            pointView.transform.position = GetSpawnPoint();
            _spawnedPoints.Add(pointView);
            pointView.gameObject.SetActive(true);
            pointView.Init(randomPoint, _provider.Get(randomPoint));
            OnSpawned?.Invoke(pointView);
        }

        protected abstract Vector2 GetSpawnPoint();
    }
}
