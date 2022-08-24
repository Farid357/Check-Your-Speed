namespace CheckYourSpeed.Utils
{
    public static class IntUlils
    {
        public static bool InPlusEqual(this int number, in int maxNumber, in int equalNumber)
        {
            for (int plusNumber = 0; plusNumber < maxNumber; plusNumber += number)
            {
                if (plusNumber == equalNumber)
                {
                    return true;
                }

                if (plusNumber > equalNumber)
                {
                    return false;
                }
            }

            return false;
        }

        public static int TryThrowLessOrEqualsToZeroException(this int number)
        {
            if (number <= 0)
                throw new LessOrEqualsToZeroException(nameof(number));

            return number;
        }
    }
}
