namespace CheckYourSpeed.Shop
{
    public interface IGoodView
    {
        public void Select();

        public void UnSelect();

        public IGood Good { get; }

    }
}