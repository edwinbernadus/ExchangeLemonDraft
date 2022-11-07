using NBitcoin;
using System.Collections.Generic;

namespace BlueLight.Main
{

    public class BtcTransaction
    {
        public Money Balance { get; set; }
        //public List<BtcTransactionDetail> Details { get; set; }
        public List<string> Details { get; set; }
    }

}