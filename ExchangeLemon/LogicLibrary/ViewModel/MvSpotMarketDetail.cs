using BlueLight.Main;

namespace BlueLight.Main
{
    public class MvSpotMarketDetail 
    {
        public SpotMarket SpotMarket { get; set; }
        public decimal MyBalance { get; set; }
        public decimal CalculateVolume { get; set; }
        public decimal CalculateLastChange { get; set; }
        public decimal PreviousLastRate { get; set; }
    }
}
