using System;
using System.Collections.Generic;
using System.Text;

namespace BotWalletWatcherLibrary
{

    public class WatcherTransaction
    {
        public int Id { get; set; }
        public string TransactionId { get; set; }
        public int Confirmations { get; set; }
        public int BlockChainId { get; set; }
        public bool IsSend { get; set; }
    }
}
