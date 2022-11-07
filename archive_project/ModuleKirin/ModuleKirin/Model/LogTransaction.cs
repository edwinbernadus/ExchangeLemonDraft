//using gekko_bot.BittrexTrade;
//using gekko_bot.WelcomeBittrexTicker;

namespace BlueLight.KirinLogic
{
    internal class LogTransaction {
        public LogTransaction () { }

        public string apikey { get; set; }
        public long nonce { get; set; }
        public string market { get; set; }
        public double quantity { get; set; }
        public double rate { get; set; }
        public string typeTransaction { get; set; }
    }
}