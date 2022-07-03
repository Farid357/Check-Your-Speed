using CheckYourSpeed.GameLogic;
using CheckYourSpeed.Model;
using UnityEngine;
using DG.Tweening;

namespace CheckYourSpeed.Root
{
    public sealed class Root : MonoBehaviour
    {
        [SerializeField] private PointsPositionsSpawner _pointsPositionsSpawner;
        [SerializeReference, SubclassSelector] private IUpdatable _updatable;
        [SerializeField] private PointsSpawner _pointsSpawner;
        [SerializeField] private ScoreView _scoreView;

        private void Start()
        {
            DOTween.Init();
            var score = new Score();
            _scoreView.Init(score);
            _pointsPositionsSpawner.Spawn();
            _pointsSpawner.Init(score, _pointsPositionsSpawner);
        }

        private void Update()
        {
            _updatable.Update(Time.deltaTime);
        }
    }
}