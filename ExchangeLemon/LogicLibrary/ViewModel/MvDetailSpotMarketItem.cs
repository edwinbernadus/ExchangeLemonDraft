using System.Collections.Generic;
using System.Data;
using System.Linq;
using BlueLight.Main;
using Newtonsoft.Json;

namespace BlueLight.Main
{
    

    public class MvDetailSpotMarketItem
    {

        //[JsonIgnore]
        //public string currentPair { get; set; }

        public int OrderId { get; set; } = -1;
        public decimal Rate { get; set; } = -1.1m;

        public decimal Amount { get; set; } = -2;
        public decimal LeftAmount { get; set; } = -3;

        public decimal TransactionAmount
        {
            get
            {
                var output = Amount - LeftAmount;
                return output;
            }
        }

        public bool IsBuy { get; set; }

        public bool IsShow { get; set; }

        public string UserName { get; set; }

        public string ShortUserName
        {
            get
            {
                var output = AccountTransaction.GetName(UserName);

                return output;
            }
        }

        public static List<MvDetailSpotMarketItem> GenerateSample()
        {
            // var output = new  List<MvDetailSpotMarketItem>();
            var output = Enumerable.Range(0, 5).Select(x => new MvDetailSpotMarketItem()).ToList();
            return output;
        }



    }
}