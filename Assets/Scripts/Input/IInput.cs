using System;

namespace CheckYourSpeed.GameLogic
{
    public interface IInput
    {
        public event Action<IPointView> OnInputed;
    }
}
