// ;
//using BlueLight.Main.ViewModel;
// using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;

namespace BlueLight.Main
{
    public class PendingBulkTransfer
    {
        public int Id { get; set; }
        public DateTimeOffset CreatedDate { get; set; } = DateTime.Now;
        public virtual ICollection<PendingTransferList> Collection { get; set; }

        public string Status { get; set; }
     }




}