using System.Collections.Generic;

namespace BlueLight.Main
{
    public class MvDetailSpotMarket {

        public string CurrencyPairName { get; set; } = "BTC/USD";
        public double LastRate {get;set;} = 0.123456;
        public double MyAvailableBalance {get;set;} = 0.32;
        public List<MvDetailSpotMarketItem> Items {get;set;} = MvDetailSpotMarketItem.GenerateSample();
    }
}
