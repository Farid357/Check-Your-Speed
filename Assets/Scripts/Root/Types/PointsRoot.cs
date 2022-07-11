using CheckYourSpeed.GameLogic;
using CheckYourSpeed.Model;
using UnityEngine;
using System.Linq;
using CheckYourSpeed.Loging;
using CheckYourSpeed.SaveSystem;
using System.Collections.Generic;
using CheckYourSpeed.App;
using CheckYourSpeed.Settings;

namespace CheckYourSpeed.Root
{
    public sealed class PointsRoot : CompositeRoot
    {
        [SerializeField] private PointsPositionsSpawner _pointsPositionsSpawner;
        [SerializeField] private WaveSpawner _waveSpawner;
        [SerializeField] private ScoreView _scoreView;
        [SerializeField] private LoseTimerView _loseTimerView;
        [SerializeField] private Waves _waves;
        [SerializeField] private WaveSpawnerView _waveSpawnerView;
        [SerializeField] private PointsSpawner _pointsSpawner;
        [SerializeField] private UserConfig _userConfig;
        [SerializeField] private SessionsCounterView _counterView;
        [SerializeField] private InputRoot _inputRoot;
        [SerializeField] private DifficultyConfig _difficultyConfig;

        private readonly List<IDisposable> _disposables = new();
        private PointsCounter _pointsCounter;
        private LoseTimer _loseTimer;

        public override void Compose()
        {
            _loseTimer = new LoseTimer(_difficultyConfig.GetSelected().CatchTime);
            IUser user = _userConfig.GetUser();
            var gameState = new GameState(_loseTimer);
            _inputRoot.Init(gameState);
            _inputRoot.Compose();
            var sessionStorage = new SessionsCounterStorage(new PlayerPrefsStorage());
            var sessionCounter = new SessionsCounter(_loseTimer, user, sessionStorage);
            _counterView.Init(sessionCounter);
            _waves.Init(_loseTimer, _waveSpawner);
            _loseTimerView.Init(_loseTimer);
            var score = new Score();
            _pointsPositionsSpawner.Spawn();
            _scoreView.Init(score);
            _waveSpawner.Spawn(true);
            _pointsSpawner.Init(score, _waves, _pointsPositionsSpawner.Positions.ToArray());
            _pointsCounter = new(_waveSpawner, _loseTimer);
            _waveSpawnerView.Init(_waveSpawner);
            _disposables.AddRange(new List<IDisposable> { _pointsCounter, gameState });
        }

        private void Update() => _loseTimer.Update(Time.deltaTime);

        private void OnDestroy() => _disposables.ForEach(disposable => disposable.Dispose());

    }
}