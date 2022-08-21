using System;
using CheckYourSpeed.Model;

namespace CheckYourSpeed.Tests
{
    public sealed class DummyPointsSwitch : IPointsSwitch
    {
        public event Action OnDisabledAll;

        public void DisableAll()
        {

        }

        public void TryDisable(int count)
        {

        }
    }
}