using BlueLight.Main;

namespace BlueLight.Main
{



    public class MvPendingOrder : MvDetailSpotMarketItem
    {
        public string CurrencyPair { get; set; }
        public Order Order { get; set; }

        public MvPendingOrder()
        {

        }
        public MvPendingOrder(Order input)
        {
            var x = input;
            Amount = x.Amount;
            OrderId = x.Id;
            Rate = x.RequestRate;
            IsBuy = x.IsBuy;
            LeftAmount = x.LeftAmount;
        }
    }
}
