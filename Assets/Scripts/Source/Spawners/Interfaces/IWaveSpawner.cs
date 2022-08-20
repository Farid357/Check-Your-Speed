using CheckYourSpeed.Model;
using System;
using System.Collections.Generic;

namespace CheckYourSpeed.GameLogic
{
    public interface IWaveSpawner
    {
        public event Action OnSpawningNextWave;

        public event Action<Wave, IEnumerable<IPointView>> OnChangedWave;

        public event Action OnWaiting;

    }
}
