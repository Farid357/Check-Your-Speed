using System.Collections.Generic;
using CheckYourSpeed.Model;

namespace CheckYourSpeed.Root
{
    public sealed class DummyPointsContainer : IPointsContainer
    {
        public void Add(IPointView point)
        {
            
        }

        public IEnumerable<IPointView> All { get; }
    }
}