using UnityEngine;
using CheckYourSpeed.Utils;
using CheckYourSpeed.Model;
using System;
using System.Collections.Generic;
using IDisposable = CheckYourSpeed.Model.IDisposable;

namespace CheckYourSpeed.GameLogic
{
    public sealed class PointsSpawner : MonoBehaviour, IDisposable
    {
        [SerializeField] private PointView _prefab;
        [SerializeField] private int _startCount = 5;
        [SerializeField] private PointColorProvider _provider;
        private readonly List<IPointView> _spawnedPoints = new();
        private Vector2[] _positions;
        private IPointsSubscriber _pointsSubscriber;
        private Waves _waves;
        private ObjectPool<PointView> _pool;

        public void Init(IPointsSubscriber pointsSubscriber, Waves waves, Vector2[] positions)
        {
            _positions = positions ?? throw new ArgumentNullException(nameof(positions));
            _pointsSubscriber = pointsSubscriber ?? throw new ArgumentNullException(nameof(pointsSubscriber));
            _waves = waves ?? throw new ArgumentNullException(nameof(waves));
            _pool = new ObjectPool<PointView>(_startCount, _prefab, transform);
        }

        public void Dispose() => _spawnedPoints.ForEach(point => point.Disable());

        public void Spawn(Wave wave)
        {
            var pointView = _pool.Get(_prefab);
            var randomPoint = _waves.GetRandomPoint(wave);
            _pointsSubscriber.Subscribe(randomPoint);
            pointView.transform.position = new Vector2().GetRandomPointFrom(_positions);
            _spawnedPoints.Add(pointView);
            pointView.gameObject.SetActive(true);
            pointView.SetColor(_provider.Get(randomPoint));
            pointView.Init(randomPoint);
        }
    }
}
