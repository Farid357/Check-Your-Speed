namespace CheckYourSpeed.Utils
{
    public static class FloatUtils
    {
        public static float TryThrowLessOrEqualsToZeroException(this float number)
        {
            if (number <= 0)
                throw new LessOrEqualsToZeroException(nameof(number));

            return number;
        }
    }
}
