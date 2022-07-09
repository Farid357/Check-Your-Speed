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
        [SerializeField] private PointsSpawner _pointsSpawner;
        private LoseTimer _loseTimer;
        private PointsCounter _pointsCounter;

        public override void Compose()
        {
            _loseTimer = new LoseTimer(_catchTime);
            _waves.Init(_loseTimer, _waveSpawner);
            _loseTimerView.Init(_loseTimer);
            var score = new Score();
            _pointsPositionsSpawner.Spawn();
            _scoreView.Init(score);
            _waveSpawner.Spawn(true);
            _pointsSpawner.Init(score, _waves, _pointsPositionsSpawner.Positions.ToArray());
            _pointsCounter = new(_waveSpawner, _loseTimer);
            _waveSpawnerView.Init(_waveSpawner);
        }

        private void Update()
        {
            _loseTimer.Update(Time.deltaTime);       
        }

        private void OnDestroy()
        {
            _pointsCounter.Dispose();
        }
    }
}