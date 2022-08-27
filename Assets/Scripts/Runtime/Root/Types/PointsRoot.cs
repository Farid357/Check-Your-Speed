using CheckYourSpeed.GameLogic;
using CheckYourSpeed.Model;
using UnityEngine;
using System.Linq;
using CheckYourSpeed.Loging;
using System.Collections.Generic;
using CheckYourSpeed.App;
using CheckYourSpeed.Settings;
using CheckYourSpeed.Factory;
using CheckYourSpeed.SaveSystem;
using CheckYourSpeed.Utils;

namespace CheckYourSpeed.Root
{
    public sealed class PointsRoot : CompositeRoot
    {
        [SerializeField] private PointsPositionsSpawner _pointsPositionsSpawner;
        [SerializeField] private WaveSpawner _waveSpawner;
        [SerializeField] private TimerFinishedCountdownVisualization _loseTimerView;
        [SerializeField] private Waves _waves;
        [SerializeField] private WaveSpawnerView _waveSpawnerView;
        [SerializeField] private UserConfig _userConfig;
        [SerializeField, RequireInterface(typeof(IVisualization<int>))] private MonoBehaviour _counterView;
        [SerializeField] private InputRoot _inputRoot;
        [SerializeField] private DifficultyConfig _difficultyConfig;
        [SerializeField] private PointsInAreaSpawner _pointsInAreaSpawner;
        [SerializeField] private ScoreRoot _scoreRoot;
        [SerializeField] private PointsRandomPositionsSpawner _randomPositionsSpawner;
        [SerializeField, RequireInterface(typeof(IVisualization<float>))] private MonoBehaviour _timerView;
        [SerializeField] private WalletRoot _walletRoot;

        private readonly PointsSwitch _pointsSwitch = new();
        private readonly List<IDisposable> _disposables = new();
        private readonly List<IUpdateble> _updatebles = new();
        private readonly PauseBroadcaster _pauseBroadcaster = new();

        public override void Compose()
        {
            _waveSpawner.Init(_waveSpawnerView);
            var timer = new Timer(_difficultyConfig.GetSelected().CatchTime, _timerView.ToInterface<IVisualization<float>>());
            var losePause = new LosePause(_pauseBroadcaster, timer);
            _inputRoot.Init(_pauseBroadcaster);
            _inputRoot.Compose();

            IUserCounterStorage sessionStorage = new FakeUserCounterStorage();

            if (_userConfig.TryLoad(out var userWithAccount))
            {
                sessionStorage = new UserCounterStorage<SessionsCounter, int, BinaryStorage>(userWithAccount);
            }

            var sessionCounter = new SessionsCounter(timer, sessionStorage, (IVisualization<int>)_counterView);
            var pointsFactory = new PointsFactory(timer, _pointsSwitch, _pointsInAreaSpawner);
            _waves.Init(pointsFactory);
            _loseTimerView.Init(timer);
            var score = _scoreRoot.Compose(_userConfig);
            var wallet = _walletRoot.Compose();
            var moneyWithChanceAdder = new MoneyWithConstantChanceAdder(wallet, 1);
            var pointsSubscribers = new IPointsSubscriber[] { score, moneyWithChanceAdder };
            _randomPositionsSpawner.Init(_pointsSwitch, pointsSubscribers, _waves);
            _pointsInAreaSpawner.Init( _pointsSwitch, pointsSubscribers, _waves);
            _pointsPositionsSpawner.Spawn();
            _waveSpawner.Spawn(true);
            _randomPositionsSpawner.Init(_pointsPositionsSpawner.Positions.ToArray());
            var pointsCounter = new PointsCounter(_waveSpawner, timer, _pointsSwitch);
            _disposables.AddRange(pointsCounter, moneyWithChanceAdder);
            _updatebles.AddRange(losePause, timer, sessionCounter);

        }

        private void Update()
        {
            if (_pauseBroadcaster.GameIsPaused == false)
            {
                _updatebles.ForEach(updateble => updateble.Update(Time.deltaTime));
            }
        }

        private void OnDestroy() => _disposables.ForEach(disposable => disposable.Dispose());

    }
}