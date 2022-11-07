using System.Collections.Generic;

namespace BotWalletWatcher
{
    public class TransactionResult
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public virtual ICollection<TransactionAddress> Addreses { get; set; }
    }
}
