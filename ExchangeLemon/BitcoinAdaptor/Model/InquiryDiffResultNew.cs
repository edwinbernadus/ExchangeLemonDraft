using NBitcoin;
using System.Collections.Generic;

namespace BlueLight.Main
{
    public class InquiryDiffResultNew
    {
        public bool HasNewItem { get; set; }
        public string Address { get; set; }
        public decimal NewDiffAmount { get; set; }

        //public List<BtcTransactionDetail> NewTransactions { get; set; }
        public List<string> NewTransactions { get; set; }
    }
}
