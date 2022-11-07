using System;
using System.Collections.Generic;
using System.Text;

namespace BotWalletWatcherLibrary
{

    public class WatcherBlockChain
    {
        public int Id { get; set; }
        public int BlockChainNumber { get; set; }
        public int Confirmations { get; set; }
        public bool IsFinish { get; set; }
        //public virtual ICollection<WatcherTransaction> Transactions { get; set; }
        //public virtual ICollection<WatcherAddresss> AddressList { get; set; }

    }
}
