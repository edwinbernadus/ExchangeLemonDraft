using System;
using System.Collections.Generic;

namespace BlueLight.Main
{
    public class WatcherReceiveLog
    {
        public int Id { get; set; }
        public int BlockChainId { get; set; }
        public virtual ICollection<WatcherReceiveLogDetail> Details { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }


}

