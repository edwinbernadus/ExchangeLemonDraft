using System;
using System.Collections.Generic;
using System.Text;

namespace BotWalletWatcherLibrary
{

    public class WatcherAddressItem
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public int BlockChainId { get; set; }
        public bool IsSend { get; set; }
    }
}
