using System;
using System.Linq;
using CheckYourSpeed.Factory;
using CheckYourSpeed.GameLogic;
using CheckYourSpeed.Loging;
using CheckYourSpeed.Model;
using UnityEngine;

namespace CheckYourSpeed.Root
{
    public sealed class BonusSceneRoot : CompositeRoot
    {
        [SerializeField] private PointsPositionsSpawner _pointsPositionsSpawner;
        [SerializeField] private PointsPositionSpawner _pointsSpawner;
        [SerializeField] private Waves _waves;
        [SerializeField] private ScoreRoot _scoreRoot;
        [SerializeField] private UserConfig _userConfig;
        private bool _hasNotSpawned = true;

        public override void Compose()
        {
            _waves.Init(new PointsFactory(new DummyTimer(), new PointsSwitch(), _pointsSpawner));
            _pointsPositionsSpawner.Spawn();
            var score = _scoreRoot.Compose(_userConfig);
            _pointsSpawner.Init(new DummyPointsContainer(),new IPointsSubscriber[]{score}, _waves);
            _pointsSpawner.Init(_pointsPositionsSpawner.Positions);
        }

        private void Update()
        {
            if (_hasNotSpawned)
            {
                for (int i = 0; i < _pointsPositionsSpawner.Positions.Count(); i++)
                {
                    _pointsSpawner.SpawnRandomFrom(new PointType[] { PointType.Standart });
                }

                _hasNotSpawned = false;
            }
        }
    }
}
