using BlueLight.Main;

namespace BlueLight.Main
{
    public class MvPendingOrderExt
    {
        public string CurrencyPair { get; set; }
        public string DisplayRate { get; set; }
        public decimal Amount { get; set; }
        public decimal Left { get; set; }
        public bool  Buy { get; set; }
        public int Id { get;  set; }

        public MvPendingOrderExt(Order order)
        {
            var x = order;
            Amount = x.Amount;
            CurrencyPair = x.CurrencyPair;
            Buy = x.IsBuy;
            DisplayRate = DisplayHelper.RateDisplay(x.RequestRate);
            Left = x.LeftAmount;
            Id = x.Id;
        }
    }
}
