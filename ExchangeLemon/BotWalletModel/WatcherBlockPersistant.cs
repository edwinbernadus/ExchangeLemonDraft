using System.Collections.Generic;
using BotWalletWatcherLibrary;

namespace BotWalletWatcher
{
    public class WatcherBlockPersistant
    {
        public WatcherBlockChain WatcherBlockChain { get; set; }
        public List<WatcherTransaction> SentItems { get; set; }
        public List<WatcherAddressItem> ReceiveItems { get; set; }
    }
}
