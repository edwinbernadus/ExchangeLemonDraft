using NBitcoin;
using System.Collections.Generic;

namespace BlueLight.Main
{
    public class InquiryDiffResult
    {
        public bool HasNewItem { get; set; }
        public string Address { get; set; }
        public int NewLastPosition { get; set; }
        public decimal NewDiffAmount { get; set; }

        public List<uint256> Transactions { get; set; }
    }
}
