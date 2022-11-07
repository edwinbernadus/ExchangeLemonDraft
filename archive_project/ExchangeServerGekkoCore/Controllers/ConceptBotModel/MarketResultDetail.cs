using System;
using Newtonsoft.Json;
//using gekko_bot.BittrexTrade;
//using gekko_bot.WelcomeBittrexTicker;

namespace BlueLight.Main {
    public class MarketResultDetail {
        [JsonProperty ("uuid")]
        public object Uuid { get; set; }

    }
}