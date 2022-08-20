using CheckYourSpeed.GameLogic;
using CheckYourSpeed.Model;
using UnityEngine;
using System.Linq;
using CheckYourSpeed.Loging;
using CheckYourSpeed.SaveSystem;
using System.Collections.Generic;
using CheckYourSpeed.App;
using CheckYourSpeed.Settings;
using UniRx;
using CheckYourSpeed.Factory;
using CheckYourSpeed.Utils;

namespace CheckYourSpeed.Root
{
    public sealed class PointsRoot : CompositeRoot
    {
        [SerializeField] private PointsPositionsSpawner _pointsPositionsSpawner;
        [SerializeField] private WaveSpawner _waveSpawner;
        [SerializeField] private ScoreView _scoreView;
        [SerializeField] private TimerFinishedCountdownView _loseTimerView;
        [SerializeField] private Waves _waves;
        [SerializeField] private WaveSpawnerView _waveSpawnerView;
        [SerializeField] private UserConfig _userConfig;
        [SerializeField] private TextView _counterView;
        [SerializeField] private InputRoot _inputRoot;
        [SerializeField] private DifficultyConfig _difficultyConfig;
        [SerializeField] private PointsInAreaSpawner _pointsInAreaSpawner;
        [SerializeField] private ScoreRoot _scoreRoot;
        [SerializeField] private IPointsSwitch _pointsSwitch;
        [SerializeField] private PointsRandomPositionsSpawner _randomPositionsSpawner;

        private readonly List<IDisposable> _disposables = new();
        private readonly List<IUpdateble> _updatebles = new();
        private readonly PauseBroadcaster _pause = new();
        private PointsCounter _pointsCounter;
        private Timer _timer;

        public override void Compose()
        {
            var pointsSwitch = new PointsSwitch();
            _waveSpawner.Init(pointsSwitch);
            _timer = new Timer(_difficultyConfig.GetSelected().CatchTime);
            var gameState = new GameState(_pause, _timer);
            IUser user = _userConfig.GetUser();
            _inputRoot.Init(_pause);
            _inputRoot.Compose();
            var sessionStorage = new SessionsCounterStorage(new BinaryStorage());
            var sessionCounter = new SessionsCounter(_timer, user, sessionStorage, _counterView);
            _waves.Init(new PointsFactory(_timer, _waveSpawner, _pointsSwitch, _pointsInAreaSpawner));
            _loseTimerView.Init(_timer);
            var score = _scoreRoot.Compose();
            _randomPositionsSpawner.Init(score, pointsSwitch, _waves);
            _pointsInAreaSpawner.Init(score, pointsSwitch, _waves);
            _pointsPositionsSpawner.Spawn();
            _scoreView.Init(score);
            _waveSpawner.Spawn(true);
            _randomPositionsSpawner.Init(_pointsPositionsSpawner.Positions.ToArray());
            _pointsCounter = new(_waveSpawner, _timer);
            _waveSpawnerView.Init(_waveSpawner);
            _disposables.AddRange(_pointsCounter);
            _updatebles.AddRange(sessionCounter, gameState);

        }

        private void Update()
        {
            if (_pause.IsPaused == false)
                _timer.Update(Time.deltaTime);

            _updatebles.ForEach(updateble => updateble.Update(Time.deltaTime));
        }

        private void OnDestroy() => _disposables.ForEach(disposable => disposable.Dispose());

    }
}