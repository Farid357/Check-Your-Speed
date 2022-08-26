using CheckYourSpeed.Model;

namespace CheckYourSpeed.Tests
{
    public sealed class DummyCountVisualization : IVisualization<int>
    {
        public int Count { get; private set; }

        public void Visualize(int count)
        {
            Count = count;
        }
    }
}