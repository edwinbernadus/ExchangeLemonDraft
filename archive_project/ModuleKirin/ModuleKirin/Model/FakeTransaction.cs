//using gekko_bot.BittrexTrade;
//using gekko_bot.WelcomeBittrexTicker;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlueLight.KirinLogic
{
    public class FakeTransaction {
        public int Id { get; set; }
        public DateTime CreatedDate = DateTime.Now;
        public double Amount { get; set; }
        public double RunningBalance { get; set; }
        public string UserName { get; set; }
        public string CurrencyCode { get; set; }
    }
}