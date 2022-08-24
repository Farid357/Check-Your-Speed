namespace CheckYourSpeed.Shop
{
    public interface IGood
    {
        public int Price { get; }

        public string Name { get; }

        public void Use();

    }
}