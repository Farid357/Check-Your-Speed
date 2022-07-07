using CheckYourSpeed.GameLogic;
using CheckYourSpeed.Model;
using UnityEngine;
using System.Linq;

namespace CheckYourSpeed.Root
{
    public sealed class PointsRoot : CompositeRoot
    {
        [SerializeField] private PointsPositionsSpawner _pointsPositionsSpawner;
        [SerializeField] private WaveSpawner _waveSpawner;
        [SerializeField] private ScoreView _scoreView;
        [SerializeField] private LoseTimerView _loseTimerView;
        [SerializeField] private Waves _waves;
        [SerializeField] private float _catchTime = 1.5f;
        [SerializeField] private WaveSpawnerView _waveSpawnerView;

        public override void Compose()
        {
            var loseTimer = new LoseTimer(_catchTime);
            _waves.Init(loseTimer, _waveSpawner);
            _loseTimerView.Init(loseTimer);
            var score = new Score();
            _pointsPositionsSpawner.Spawn();
            _scoreView.Init(score);
            _waveSpawnerView.Init(_waveSpawner);
            _waveSpawner.Init(score, loseTimer, _pointsPositionsSpawner.Positions.ToArray());
        }
    }
}