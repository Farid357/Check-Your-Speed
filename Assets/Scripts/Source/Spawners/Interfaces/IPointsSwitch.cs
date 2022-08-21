using System;

namespace CheckYourSpeed.Model
{
    public interface IPointsSwitch
    {
        public void DisableAll();

        public void TryDisable(int count);

        public event Action OnDisabledAll;

    }
}
