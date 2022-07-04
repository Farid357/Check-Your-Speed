using CheckYourSpeed.GameLogic;
using CheckYourSpeed.Model;
using UnityEngine;
using DG.Tweening;
using System.Linq;

namespace CheckYourSpeed.Root
{
    public sealed class Root : MonoBehaviour
    {
        [SerializeField] private PointsPositionsSpawner _pointsPositionsSpawner;
        [SerializeField] private WaveSpawner _waveSpawner;
        [SerializeField] private ScoreView _scoreView;
        [SerializeField] private LoseTimerView _loseTimerView;
        [SerializeField] private Waves _waves;
        [SerializeField] private float _catchTime = 1.5f;
        [SerializeField] private WaveSpawnerView _waveSpawnerView;
        private IUpdatable _updatable;
        private IPointsSubscriber _score;
        private ILoseTimer _loseTimer;

        private void Awake()
        {
            _loseTimer = new LoseTimer(_catchTime);
            _waves.Init(_loseTimer);
            _updatable = new PointerInput(Camera.main);
            DOTween.Init();
            _loseTimerView.Init(_loseTimer as LoseTimer);
            _score = new Score();
            _pointsPositionsSpawner.Spawn();
            _scoreView.Init(_score as Score);
            _waveSpawnerView.Init(_waveSpawner);
        }

        private void Start()
        {
            _waveSpawner.Init(_score, _loseTimer, _pointsPositionsSpawner.Positions.ToArray());
        }

        private void Update()
        {
            _updatable.Update(Time.deltaTime);
        }
    }
}