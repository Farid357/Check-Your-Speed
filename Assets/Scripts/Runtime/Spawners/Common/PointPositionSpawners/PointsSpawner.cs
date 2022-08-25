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
        private IPointsSubscriber _pointsSubscriber;
        private IndependentPool<PointView> _pool;
        private IPointsContainer _pointsContainer;
        private Waves _waves;

        public IEnumerable<IPointView> SpawnedPoints => _pointsContainer.All;

        public void Init(IPointsSubscriber pointsSubscriber, IPointsContainer pointsContainer, Waves waves)
        {
            _pointsSubscriber = pointsSubscriber ?? throw new ArgumentNullException(nameof(pointsSubscriber));
            _waves = waves ?? throw new ArgumentNullException(nameof(waves));
            _pointsContainer = pointsContainer ?? throw new ArgumentNullException(nameof(pointsContainer));
            _pool = new IndependentPool<PointView>(new GameObjectsFactory<PointView>(_prefab, transform));
        }

        public void SpawnRandomFrom(PointType[] pointTypes)
        {
            if (pointTypes is null)
            {
                throw new ArgumentNullException(nameof(pointTypes));
            }

            var pointView = _pool.Get();
            var randomPoint = _waves.CreateRandomPoint(pointTypes);
            pointView.gameObject.SetActive(true);
            _pointsSubscriber.Subscribe(randomPoint);
            pointView.transform.position = GetSpawnPoint();
            pointView.Init(randomPoint, _colorFactory.CreateFrom(randomPoint));
            _pointsContainer.Add(pointView);
        }

        private void Update() => _pool.Update(Time.deltaTime);

        protected abstract Vector2 GetSpawnPoint();
    }
}
