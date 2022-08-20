using UnityEngine;
using CheckYourSpeed.Utils;
using CheckYourSpeed.Model;
using System;
using System.Collections.Generic;
using CheckYourSpeed.Factory;

namespace CheckYourSpeed.GameLogic
{
    public abstract class PointsSpawner : MonoBehaviour, IPointsSpawner
    {
        [SerializeField] private PointView _prefab;
        [SerializeField] private PointColorFactory _colorFactory;
        private readonly List<IPointView> _spawnedPoints = new();
        private IPointsSubscriber _pointsSubscriber;
        private IPool<PointView> _pool;
        private IPointsContainer _pointsContainer;
        private Waves _waves;

        public IEnumerable<IPointView> SpawnedPoints => _spawnedPoints;

        public void Init(IPointsSubscriber pointsSubscriber, IPointsContainer pointsContainer, Waves waves)
        {
            _pointsSubscriber = pointsSubscriber ?? throw new ArgumentNullException(nameof(pointsSubscriber));
            _waves = waves ?? throw new ArgumentNullException(nameof(waves));
            _pointsContainer = pointsContainer ?? throw new ArgumentNullException(nameof(pointsContainer));
            _pool = new Pool<PointView>(new GameObjectsFactory<PointView>(_prefab, transform));
        }

        public void Spawn(PointType[] pointTypes)
        {
            if (pointTypes is null)
            {
                throw new ArgumentNullException(nameof(pointTypes));
            }

            var pointView = _pool.Get();
            var randomPoint = _waves.CreateRandomPoint(pointTypes);
            _pointsSubscriber.Subscribe(randomPoint);
            pointView.transform.position = GetSpawnPoint();
            _spawnedPoints.Add(pointView);
            pointView.Init(randomPoint, _colorFactory.CreateFrom(randomPoint));
            _pointsContainer.Add(pointView);
        }

        protected abstract Vector2 GetSpawnPoint();
    }
}
