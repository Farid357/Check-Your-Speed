using System;

namespace CheckYourSpeed.Model
{
    public interface IPoint
    {
        public void Apply();

        public event Action<IPoint> OnApplyed;
    }
}