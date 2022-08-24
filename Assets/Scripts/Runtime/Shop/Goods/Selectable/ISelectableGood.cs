namespace CheckYourSpeed.Shop.Model
{
    public interface ISelectableGood
    {
        public IGood Good { get; }

        public void Select();

        public void Unselect();

        public void VisualizeBuying();

    }
}