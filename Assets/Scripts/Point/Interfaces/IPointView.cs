using System;

namespace CheckYourSpeed.GameLogic
{
    public interface IPointView
    {
        public void Apply();

        public void Disable();

        public event Action OnDisabled;

    }
}
