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
        [SerializeField] private PointDictionary _points;
        private IPointsSubscriber[] _pointsSubscribers;
        private Dictionary<PointType, IndependentPool<PointView>> _pools;
        private IPointsContainer _pointsContainer;
        private Waves _waves;

        public IEnumerable<IPointView> SpawnedPoints => _pointsContainer.All;

        public void Init(IPointsContainer pointsContainer, IPointsSubscriber[] pointsSubscribers, Waves waves)
        {
            _waves = waves ?? throw new ArgumentNullException(nameof(waves));
            _pointsSubscribers = pointsSubscribers ?? throw new ArgumentNullException(nameof(pointsSubscribers));
            _pointsContainer = pointsContainer ?? throw new ArgumentNullException(nameof(pointsContainer));
            _pools = new Dictionary<PointType, IndependentPool<PointView>>(_points.Count);
            for (int i = 0; i < _points.Count; i++)
            {
                _pools[_points.Keys[i]] =
                    new IndependentPool<PointView>(new GameObjectsFactory<PointView>(_points[i], transform));
            }
        }

        public void SpawnRandomFrom(PointType[] pointTypes)
        {
            if (pointTypes is null)
            {
                throw new ArgumentNullException(nameof(pointTypes));
            }

            var pointType = new System.Random().GetRandomFromArray(pointTypes);
            var pointView = _pools[pointType].Get();
            var randomPoint = _waves.CreateRandomPoint(pointTypes);
            pointView.Init(randomPoint);
            pointView.gameObject.SetActive(true);
            _pointsSubscribers.ForEach(subscriber => subscriber.Subscribe(randomPoint));
            pointView.transform.position = GetSpawnPoint();
            _pointsContainer.Add(pointView);
        }

        private void Update() => _pools.ForEach(pool => pool.Value.Update(Time.deltaTime));

        protected abstract Vector2 GetSpawnPoint();
    }
}