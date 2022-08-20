using System;

namespace CheckYourSpeed.Model
{
    public interface IPointInput
    {
        public event Action<IPointView> OnInputed;
    }
}
