using System.Collections.Generic;

namespace BotWalletWatcher
{
    public class BlockChainList
    {
        public int Id { get; set; }
        public int BlockChainNumber { get; set; }
        public virtual ICollection<TransactionList> TransactionLists { get; set; }
        public virtual ICollection<TransactionResult> TransactionDetailListss { get; set; }
    }
}
