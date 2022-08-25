using CheckYourSpeed.Utils;

namespace CheckYourSpeed.Model
{
    public  sealed  class  MoneyFactor : IMoneyFactor
    {
        private readonly int _factor;

        public MoneyFactor(int factor)
        {
            _factor = factor.TryThrowLessThanOrEqualsToZeroException();
        }

        public int TryIcrease(int money) => money * _factor;
        
    }
}