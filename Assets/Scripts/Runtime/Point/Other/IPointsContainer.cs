using System.Collections.Generic;

namespace CheckYourSpeed.Model
{
    public interface IPointsContainer
    {
        public void Add(IPointView point);

        public IEnumerable<IPointView> All { get; }

    }
}
