using System.Linq;

namespace BlueLight.Main
{

    public class ValidationBusinessLogic
    {
        public bool CheckBalanceSubmitOrder(UserProfile user, Order order)
        {
            var userProfileDetails = user.UserProfileDetails;

            
            var amount = AmountCalculator.Calc(order.Amount , order.RequestRate);
            var currencyPair = order.CurrencyPair;

            var secondPair = PairHelper.GetSecondPair(currencyPair);
            var currencyCode = secondPair;

            var detail = userProfileDetails.First(x => x.CurrencyCode == currencyCode);
            if (UserProfileLogic.GetAvailableBalance(detail) < amount)
            {
                return false;
            }
            else
            {
                return true;
            }

        }
    }
}