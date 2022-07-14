using Zenject;
using UnityEngine;
using CheckYourSpeed.Model;

namespace CheckYourSpeed.GameLogic
{
    public sealed class AppStartBindings : MonoInstaller
    {
        [SerializeField] private PointsRandomPositionsSpawner _randomPositionsSpawner;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<PointsRandomPositionsSpawner>().FromInstance(_randomPositionsSpawner).AsSingle();
            var pointsSwicth = new PointsSwitch(_randomPositionsSpawner);
            Container.BindInterfacesAndSelfTo<PointsSwitch>().FromInstance(pointsSwicth).AsSingle();
        }
    }
}
