using System;
using System.Collections.Generic;

namespace BlueLight.Main
{
    public class WatcherSendLog
    {
        //int blockChainId, List<string> transactionLists, int confirmations
        public int Id { get; set; }
        public int BlockChainId { get; set; }
        public int Confirmations { get; set; }
        public virtual ICollection<WatcherSendLogDetail> Details { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }


}

