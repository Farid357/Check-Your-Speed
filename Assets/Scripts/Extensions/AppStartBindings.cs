using Zenject;
using UnityEngine;
using CheckYourSpeed.Model;

namespace CheckYourSpeed.GameLogic
{
    public sealed class AppStartBindings : MonoInstaller
    {
        [SerializeField] private WaveSpawner _waveSpawner;
        [SerializeField] private float _catchTime = 1.5f;

        public override void InstallBindings()
        {
            var timer = new LoseTimer(_catchTime);
            Container.BindInterfacesTo<LoseTimer>().FromInstance(timer).AsSingle();
        }
    }
}
