using CheckYourSpeed.Model;

namespace CheckYourSpeed.Factory
{
    public interface IFactory
    {
        public abstract IPoint Get();
    }
}